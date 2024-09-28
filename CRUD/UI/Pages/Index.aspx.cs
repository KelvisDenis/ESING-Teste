using CRUD.Domain.Entities.Models;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            var people = new List<PeopleSalary>();

            string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT \"ID\", \"Nome\", \"Salario\" FROM public.\"Pessoa_Salario\"";

               

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    

                    using (var reader =  command.ExecuteReader())
                    {


                        while (reader.Read())
                        {

                            var pessoa = new PeopleSalary
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),    
                                Salary = reader.GetInt32(2)    
                            };
                            people.Add(pessoa);
                        }
                    }
                }
            }

            // Bind the retrieved data to the repeater
            peopleRepeater.DataSource = people;
            peopleRepeater.DataBind();
        }
        // redireciona para a pagina de create
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Create.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }
    }


}
