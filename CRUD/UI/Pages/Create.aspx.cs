using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRUD.UI.Pages
{
    /// <summary>
    /// Página para criar uma nova pessoa no sistema.
    /// </summary>
    public partial class Create : System.Web.UI.Page
    {
        /// <summary>
        /// Evento executado quando a página é carregada.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nenhuma ação necessária no carregamento inicial
        }

        /// <summary>
        /// Método chamado quando o botão de criação é clicado.
        /// Ele cria uma nova pessoa e salva os dados.
        /// </summary>
        protected async void btnCreate_Click(object sender, EventArgs e)
        {
            var service = new PeopleService();

            // Captura e limpa os valores inseridos nos campos de texto
            string nome = TextBox1.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string cidade = TextBox2.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string pais = TextBox9.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string email = TextBox3.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string endereco = TextBox4.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string cep = TextBox5.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string usuario = TextBox6.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string telefone = TextBox7.Text.Trim(new char[] { ' ', '\t', '\n', '\r' });
            string dataNascimento = TextBox8.Text;

            // Obtém o valor selecionado do dropdown de cargos
            int cargo = int.Parse(DropDownListCargo.SelectedValue);

            try
            {
                // Cria um novo objeto People com os dados fornecidos
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
                    DataNascimento = DateTime.Parse(dataNascimento), // Converte a data de nascimento
                    IDCargo = cargo
                };

                // Chama o serviço para adicionar a pessoa ao sistema
                var response = await service.AddPeopleAsync(pessoa);

                // Verifica se a criação foi bem-sucedida, caso contrário, lança uma exceção
                if (!response) throw new Exception("Erro ao adicionar pessoa");

                // Redireciona para a página de listagem após a criação bem-sucedida
                Response.Redirect("~/UI/Pages/Index.aspx", false);
            }
            catch (Exception ex)
            {
                // Em caso de erro, registra a exceção e redireciona para a página de erro
                Console.WriteLine(ex);
                Response.Redirect("~/UI/Pages/ErrorPage.aspx", false);
            }
        }

        /// <summary>
        /// Método para retornar à página de listagem sem criar uma nova pessoa.
        /// </summary>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            // Redireciona de volta para a página de listagem
            Response.Redirect("~/UI/Pages/Index.aspx");
        }
    }
}
