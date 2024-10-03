using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD.UI.Pages
{
    public partial class EditSalary : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int personId = int.Parse(Request.QueryString["id"]);

                    await LoadData(personId);
                }
            }
        }
        private async Task LoadData(int id)
        {
            var service = new PeopleSalaryService();
            var peopleSalary = await service.GetPeopleSalaryByIDAsync(id);

            var servicePeople = new PeopleService();
            var people = await servicePeople.GetPeopleByNameAsync(peopleSalary.Name);

            var serviceCargo = new CargoService();
            var cargo = await serviceCargo.GetCargoByIDAsync(people.IDCargo);


            if (people != null &&  peopleSalary != null && cargo != null )
            {
                var newString = $"R$ {peopleSalary.Salary},00";
                TextBoxNome.Text = peopleSalary.Name;
                TextBoxCargo.Text = cargo.Name;
                TextBoxSalario.Text = newString;
                HiddenFieldPersonId.Value = id.ToString();

            }
        }
        protected async Task btnUpdate_Click(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();

            var id = int.Parse(HiddenFieldPersonId.Value);
            string name = TextBoxNome.Text;
            string cargo = TextBoxCargo.Text;
            string salarioFormatado = TextBoxNovoSalario.Text.Replace("R$", "").Replace(".", "").Replace(",", "");
            int salary = int.Parse(salarioFormatado);

            try
            {
                var people = new PeopleSalaryModel
                {
                    ID = id,
                    Name = name,
                    Salary = salary
                };
                var response = await service.UpdatePeopleSalaryAsync(people);

                if (!response) throw new Exception("Error ao atualizar salario");
                Response.Redirect("~/UI/Pages/Index.aspx", false);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Response.Redirect("~/UI/Pages/ErrorPage.aspx", false);
            }
            

        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Index.aspx");

        }
    }
}