using System;
using System.Web.UI;

namespace CRUD.UI.Pages
{
    /// <summary>
    /// Página que exibe uma mensagem de erro e permite ao usuário retornar à página inicial.
    /// </summary>
    public partial class ErrorPage : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que é chamado quando a página é carregada.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Aqui você pode adicionar lógica para exibir uma mensagem de erro, se necessário
        }

        /// <summary>
        /// Método chamado quando o botão "Voltar para Home" é clicado.
        /// Redireciona o usuário de volta para a página inicial.
        /// </summary>
        protected void btHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Index.aspx");
        }
    }
}
