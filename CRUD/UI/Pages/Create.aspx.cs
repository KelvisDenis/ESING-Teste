using System;
using System.Collections.Generic;
using System.Linq;
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
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string itemName = TextBox1.Text;
            if (!string.IsNullOrEmpty(itemName)) { 
                Response.Write("<script>alert('Item criado com sucesso!');</script>");
            }
            else
            {
                // Exibir uma mensagem de erro se o campo estiver vazio
                Response.Write("<script>alert('O nome do item não pode estar vazio.');</script>");
            }
        }
        // metodo para retornar para pagina index
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Index.aspx");
        }
    }
}