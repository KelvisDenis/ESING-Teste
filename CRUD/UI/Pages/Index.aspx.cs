using CRUD.Application.Service;
using CRUD.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CRUD.UI.Pages
{
    /// <summary>
    /// Página de listagem principal com funcionalidades de paginação, busca e exclusão de registros.
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {
        // Definição do tamanho da página para a paginação
        private const int PageSize = 10;

        /// <summary>
        /// Propriedade para gerenciar a página atual utilizando o ViewState.
        /// Inicia com a página 1 se não houver valor armazenado no ViewState.
        /// </summary>
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                    ViewState["CurrentPage"] = 1;
                return (int)ViewState["CurrentPage"];
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        /// <summary>
        /// Método executado quando a página é carregada.
        /// Se for o primeiro carregamento (não PostBack), carrega os dados das pessoas com paginação.
        /// </summary>
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadPeopleData();
            }
        }

        /// <summary>
        /// Carrega os dados de pessoas e salários utilizando paginação.
        /// </summary>
        protected async Task LoadPeopleData()
        {
            var service = new PeopleSalaryService();
            var peoples = await service.GetAllPeopleSalaryAsync(CurrentPage, PageSize);

            // Preenche o Repeater com os dados de pessoas
            peopleRepeater.DataSource = peoples;
            peopleRepeater.DataBind();

            // Exibe o número da página atual
            lblPageNumber.Text = "Página " + CurrentPage;
        }

        /// <summary>
        /// Evento de clique para o botão "Anterior".
        /// Move para a página anterior se não estiver na primeira página.
        /// </summary>
        protected async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadPeopleData();
            }
        }

        /// <summary>
        /// Evento de clique para o botão "Próxima".
        /// Move para a próxima página se ainda houver mais registros para carregar.
        /// </summary>
        protected async void btnNext_Click(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();
            var totalPeople = await service.GetTotalPeopleSalaryCountAsync();

            // Verifica se há mais páginas disponíveis
            if (CurrentPage < (totalPeople / PageSize))
            {
                CurrentPage++;
                await LoadPeopleData();
            }
        }

        /// <summary>
        /// Evento de clique para o botão de busca.
        /// Busca pessoas pelo nome e exibe o resultado na interface.
        /// </summary>
        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            var name = searchInput.Text.Trim(); // Remove espaços e quebras de linha
            var service = new PeopleSalaryService();
            var peoples = new List<PeopleSalaryModel>();

            if (!string.IsNullOrEmpty(name))
            {
                // Realiza a busca por nome
                var people = await service.GetPeopleSalaryByNameAsync(name);
                peoples.Add(people);

                if (people == null || peoples.Count == 0)
                {
                    peoples.Clear(); // Se não houver resultados, limpa a lista
                }
            }
            else
            {
                // Se o nome estiver vazio, recarrega os dados com paginação
                CurrentPage = 1;
                await LoadPeopleData();
                return;
            }

            // Preenche o Repeater com os dados buscados
            peopleRepeater.DataSource = peoples;
            peopleRepeater.DataBind();
        }

        /// <summary>
        /// Evento de clique para o botão de adicionar nova pessoa.
        /// Redireciona para a página de criação de pessoa.
        /// </summary>
        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Create.aspx");
        }

        /// <summary>
        /// Evento de clique para o botão de exclusão.
        /// Remove o registro de pessoa e atualiza a página.
        /// </summary>
        protected async void btnExcluir_Click(object sender, EventArgs e)
        {
            var service = new PeopleSalaryService();

            // Obtém o ID da pessoa a ser excluída a partir do argumento do botão
            var button = (Button)sender;
            int id = Convert.ToInt32(button.CommandArgument);

            // Remove a pessoa pelo ID e, se houver falha, redireciona para a página de erro
            var response = await service.RemovePeopleSalaryAsync(id);
            if (!response) Response.Redirect("~/UI/Pages/ErrorPage.aspx", false);

            // Atualiza a página após a exclusão
            Response.Redirect(Request.RawUrl);
        }
    }
}
