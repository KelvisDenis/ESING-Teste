using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRUD.UI.Pages
{
    /// <summary>
    /// Página para editar o salário de uma pessoa existente.
    /// </summary>
    public partial class EditSalary : System.Web.UI.Page
    {
        /// <summary>
        /// Evento executado quando a página é carregada.
        /// Carrega os dados do salário da pessoa para edição, se houver um ID fornecido na URL.
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

                    // Carrega os dados do salário da pessoa com o ID fornecido
                    await LoadData(personId);
                }
            }
        }

        /// <summary>
        /// Método para carregar os dados do salário da pessoa a ser editada com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID da pessoa</param>
        private async Task LoadData(int id)
        {
            var service = new PeopleSalaryService();
            // Obtém os dados do salário da pessoa pelo ID
            var peopleSalary = await service.GetPeopleSalaryByIDAsync(id);

            // Serviço para obter informações da pessoa
            var servicePeople = new PeopleService();
            var people = await servicePeople.GetPeopleByNameAsync(peopleSalary.Name);

            // Serviço para obter o cargo da pessoa
            var serviceCargo = new CargoService();
            var cargo = await serviceCargo.GetCargoByIDAsync(people.IDCargo);

            // Verifica se as informações da pessoa, do salário e do cargo estão disponíveis
            if (people != null && peopleSalary != null && cargo != null)
            {
                // Formata o salário para exibição
                var newString = $"R$ {peopleSalary.Salary},00";
                TextBoxNome.Text = peopleSalary.Name; // Preenche o nome da pessoa
                TextBoxCargo.Text = cargo.Name; // Preenche o cargo da pessoa
                TextBoxSalario.Text = newString; // Preenche o salário formatado
                HiddenFieldPersonId.Value = id.ToString(); // Armazena o ID da pessoa em um campo oculto
            }
        }

        /// <summary>
        /// Método chamado quando o botão de atualização é clicado.
        /// Ele atualiza os dados do salário da pessoa no banco de dados.
        /// </summary>
        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();

            // Captura o ID da pessoa do campo oculto
            var id = int.Parse(HiddenFieldPersonId.Value);

            // Captura os valores inseridos nos campos de texto
            string name = TextBoxNome.Text;
            string cargo = TextBoxCargo.Text;
            // Formata o novo salário removendo a formatação monetária
            string salarioFormatado = TextBoxNovoSalario.Text.Replace("R$", "").Replace(".", "").Replace(",", "");
            int salary = int.Parse(salarioFormatado); // Converte para inteiro

            try
            {
                // Cria um objeto PeopleSalaryModel com os novos dados
                var people = new PeopleSalaryModel
                {
                    ID = id, // Mantém o ID da pessoa
                    Name = name,
                    Salary = salary // Define o novo salário
                };

                // Chama o serviço para atualizar os dados do salário da pessoa
                var response = await service.UpdatePeopleSalaryAsync(people);

                // Verifica se a atualização foi bem-sucedida
                if (!response) throw new Exception("Erro ao atualizar salário");

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
