using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRUD.UI.Pages
{
    /// <summary>
    /// Página para editar uma pessoa existente.
    /// </summary>
    public partial class Edit : System.Web.UI.Page
    {
        /// <summary>
        /// Evento executado quando a página é carregada.
        /// Carrega os dados da pessoa para edição, se houver um ID fornecido na URL.
        /// </summary>
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Verifica se não é um postback para evitar recarregamento desnecessário
            if (!IsPostBack)
            {
                // Verifica se o parâmetro 'id' foi passado na URL
                if (Request.QueryString["id"] != null)
                {
                    int personId = int.Parse(Request.QueryString["id"]);

                    // Carrega os dados da pessoa com o ID fornecido
                    await LoadData(personId);
                }
            }
        }

        /// <summary>
        /// Método para carregar os dados da pessoa a ser editada com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID da pessoa</param>
        private async Task LoadData(int id)
        {
            var service = new PeopleService();

            // Obtém os dados da pessoa pelo ID
            var person = await service.GetPeopleByIDAsync(id);

            if (person != null)
            {
                // Preenche os campos da página com os dados da pessoa
                TextBox1.Text = person.Nome;
                TextBox2.Text = person.Cidade;
                TextBox9.Text = person.Pais;
                TextBox3.Text = person.Email;
                TextBox4.Text = person.Endereco;
                TextBox5.Text = person.CEP;
                TextBox6.Text = person.Usuario;
                TextBox7.Text = person.Telefone;
                TextBox8.Text = person.DataNascimento.ToString("yyyy-MM-dd"); // Define o formato para um campo de data
                HiddenFieldPersonId.Value = person.ID.ToString(); // Armazena o ID da pessoa em um campo oculto
                DropDownListCargo.SelectedValue = person.IDCargo.ToString(); // Define o cargo selecionado
            }
        }

        /// <summary>
        /// Método chamado quando o botão de editar é clicado.
        /// Ele atualiza os dados da pessoa no banco de dados.
        /// </summary>
        protected async void btnEdit_Click(object sender, EventArgs e)
        {
            var service = new PeopleService();

            // Captura o ID da pessoa do campo oculto
            int id = int.Parse(HiddenFieldPersonId.Value);

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
                // Cria um objeto People com os novos dados
                var pessoa = new People
                {
                    ID = id, // Mantém o ID da pessoa
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

                // Chama o serviço para atualizar os dados da pessoa
                var response = await service.UpdatePeopleAsync(pessoa);

                // Verifica se a atualização foi bem-sucedida
                if (!response) throw new Exception("Erro ao atualizar pessoa");

                // Redireciona para a página de listagem após a atualização bem-sucedida
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
        /// Método para retornar à página de listagem sem fazer alterações.
        /// </summary>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            // Redireciona de volta para a página de listagem
            Response.Redirect("~/UI/Pages/Index.aspx");
        }
    }
}
