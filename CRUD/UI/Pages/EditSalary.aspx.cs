using CRUD.Application.Service;
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
                var newString = $"R$ {peopleSalary.Salary}";
                TextBoxNome.Text = peopleSalary.Name;
                TextBoxCargo.Text = cargo.Name;
                TextBoxSalario.Text = newString;

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {

        }
    }
}