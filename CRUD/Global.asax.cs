using CRUD.Infrastructure.Data;  // Importa o namespace que contém a lógica para acesso a dados
using System;
using System.Web;

namespace CRUD
{
    /// <summary>
    /// Classe Global que gerencia eventos da aplicação ASP.NET.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Evento que é chamado quando a aplicação é iniciada.
        /// Aqui, você pode inicializar recursos da aplicação.
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            // Cria uma instância de CreateTableDB e chama o método para criar tabelas se não existirem
            var createTableDB = new CreateTableDB();
            createTableDB.CriarTabelasSeNaoExistirem().GetAwaiter().GetResult();
        }
    }
}
