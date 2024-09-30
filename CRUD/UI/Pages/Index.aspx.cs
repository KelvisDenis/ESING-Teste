using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            

            // Bind dos dados no Repeater
            peopleRepeater.DataSource = peoples;
            peopleRepeater.DataBind();

            // Atualizar o número da página
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

            if (!string.IsNullOrEmpty(name))
            {
                var service = new PeopleSalaryService();
                var people = await service.GetPeopleSalaryByNameAsync(name);

                var peoples = new List<PeopleSalaryModel> { people };

                peopleRepeater.DataSource = peoples;
                peopleRepeater.DataBind();
            }
            else
            {
                CurrentPage = 1; // Reseta a página ao fazer uma busca sem resultado
                await LoadPeopleData();
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Create.aspx");

        }
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Create.aspx");

        }

    }


}
