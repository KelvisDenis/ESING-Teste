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
    // pagina de criar nova pessoa
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // metodo para criar nova pessoa
        protected async void btnCreate_Click(object sender, EventArgs e)
        {
            var service = new PeopleService();

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
                var response = await service.AddPeopleAsync(pessoa);
                if (!response) throw new Exception("Erro ao adicionar pessoa");
                Response.Redirect("~/UI/Pages/Index.aspx", false);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Response.Redirect("~/UI/Pages/ErrorPAge.aspx", false);
            }
            


        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Index.aspx");
        }
    }
}