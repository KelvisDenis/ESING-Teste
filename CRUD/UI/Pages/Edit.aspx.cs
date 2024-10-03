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
                TextBox9.Text = person.Pais;
                TextBox3.Text = person.Email;
                TextBox4.Text = person.Endereco;
                TextBox5.Text = person.CEP;
                TextBox6.Text = person.Usuario;
                TextBox7.Text = person.Telefone;
                TextBox8.Text = person.DataNascimento.ToString("yyyy-MM-dd");
                HiddenFieldPersonId.Value = person.ID.ToString();
                DropDownListCargo.SelectedValue = person.IDCargo.ToString();
            }
        }
        protected async Task btnEdit_Click(object sender, EventArgs e)
        {
            var service = new PeopleService();
            int id = int.Parse(HiddenFieldPersonId.Value);
            string nome = TextBox1.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string cidade = TextBox2.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string pais = TextBox9.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string email = TextBox3.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string endereco = TextBox4.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string cep = TextBox5.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string usuario = TextBox6.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string telefone = TextBox7.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string dataNascimento = TextBox8.Text;

            int cargo = int.Parse(DropDownListCargo.SelectedValue);

            try
            {
                var pessoa = new People
                {
                    ID = id,
                    Nome = nome,
                    Cidade = cidade,
                    Pais = pais,
                    Email = email,
                    Endereco = endereco,
                    CEP = cep,
                    Usuario = usuario,
                    Telefone = telefone,
                    DataNascimento = DateTime.Parse(dataNascimento),
                    IDCargo = cargo

                };

                var response = await service.UpdatePeopleAsync(pessoa);
                if (!response) throw new Exception("Error ao atualizar pessoa");
                    Response.Redirect("~/UI/Pages/Index.aspx", false);
            }
            catch (Exception ex)
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