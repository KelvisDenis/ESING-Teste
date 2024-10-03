using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CRUD.UI.Pages
{
    // classe de index 
    public partial class Index : System.Web.UI.Page
    {
        private const int PageSize = 10; // Número de registros por página

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                    ViewState["CurrentPage"] = 1;
                return (int)ViewState["CurrentPage"];
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadPeopleData();
            }
        }

        // Método para carregar dados com paginação
        protected async Task LoadPeopleData()
        {
            var service = new PeopleSalaryService();
            var peoples = await service.GetAllPeopleSalaryAsync(CurrentPage, PageSize);
            
            

            peopleRepeater.DataSource = peoples;
            peopleRepeater.DataBind();

            lblPageNumber.Text = "Página " + CurrentPage;
        }

        // Botão "Anterior"
        protected async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadPeopleData();
            }
        }

        // Botão "Próxima"
        protected async void btnNext_Click(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();
            var totalPeople = (await service.GetTotalPeopleSalaryCountAsync());

            if (CurrentPage < (totalPeople / PageSize))
            {
                CurrentPage++;
                await LoadPeopleData();
            }
        }

        // Busca por nome
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            var name = searchInput.Text;
            name = name.Trim(new char[] { ' ', '\t', '\n', '\r' });

            var service = new PeopleSalaryService();
            var peoples = new List<PeopleSalaryModel> ();

            if (!string.IsNullOrEmpty(name))
            {
                var people = await service.GetPeopleSalaryByNameAsync(name);
                peoples.Add(people);

                if (people == null || peoples.Count == 0)
                {

                    peoples.Clear();
                }
            }
            else
            {
                CurrentPage = 1; 
                await LoadPeopleData();
                return; 
            }
            peopleRepeater.DataSource = peoples;
            peopleRepeater.DataBind();
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Create.aspx");

        }
        
        protected async void btnExcluir_Click(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();

            var button = (Button)sender; 
            int id = Convert.ToInt32(button.CommandArgument);

            var response = await service.RemovePeopleSalaryAsync(id);

            if (!response) Response.Redirect("~/UI/Pages/ErrorPage.aspx", false);

            Response.Redirect(Request.RawUrl);

        }

    }


}
