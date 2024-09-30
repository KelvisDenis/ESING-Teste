using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using CRUD.Infrastructure.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD.UI.Pages
{
    // classe de index 
    public partial class Index : System.Web.UI.Page
    {
        // metodo para buscar todos os valores da tabela Pessoa_salario no BD e renderiza-lo
        protected async void Page_Load(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();
            var peoples = await service.GetAllPeopleSalaryAsync();

            peopleRepeater.DataSource = peoples;
            peopleRepeater.DataBind();
        }
        // redireciona para a pagina de create
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Create.aspx");
        }
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            var name = searchInput.Text;

            if (name != null)
            {
                var service = new PeopleSalaryService();
                var people = await service.GetPeopleSalaryByNameAsync(name);
                var peoples = new List<PeopleSalaryModel>() { people };

                peopleRepeater.DataSource = peoples;
                peopleRepeater.DataBind();
            }
            else
            {
                var service = new PeopleSalaryService();
                var peoples = service.GetAllPeopleSalaryAsync();

                peopleRepeater.DataSource = peoples;
                peopleRepeater.DataBind();
            }
           
        }
    }


}
