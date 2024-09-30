using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using CRUD.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD.UI.Pages
{
    public partial class Edit : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["id"] != null)
                {
                    int personId = int.Parse(Request.QueryString["id"]);

                   await LoadData(personId);
                }
            }

        }
        private async Task LoadData(int id)
        {
            var service = new PeopleService();
            var person = await service.GetPeopleByIDAsync(id); 

            if (person != null)
            {
                TextBox1.Text = person.Nome;
                TextBox2.Text = person.Cidade;
                TextBox3.Text = person.Email;
                TextBox4.Text = person.Endereco;
                TextBox5.Text = person.CEP;
                TextBox6.Text = person.Usuario;
                TextBox7.Text = person.Telefone;
                TextBox8.Text = person.DataNascimento.ToString("yyyy-MM-dd");
                DropDownListCargo.SelectedValue = person.IDCargo.ToString();
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Index.aspx");

        }
    }
}