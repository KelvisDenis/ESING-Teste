using CRUD.Domain.Entities.Models;  // Importa o namespace que contém os modelos de entidade
using CRUD.Infrastructure.Repositories;  // Importa o namespace que contém os repositórios
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace CRUD.Application.Service
{
    /// <summary>
    /// Classe de serviço para gerenciar operações relacionadas a salários de pessoas.
    /// </summary>
    public class PeopleSalaryService
    {
        private readonly string _connectionString;  // String de conexão com o banco de dados
        private readonly PeopleSalaryRepository _peopleSalaryRepository;  // Repositório para operações de salário de pessoas

        /// <summary>
        /// Construtor da classe PeopleSalaryService.
        /// Inicializa a string de conexão e o repositório.
        /// </summary>
        public PeopleSalaryService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;  // Obtém a string de conexão do arquivo de configuração
            _peopleSalaryRepository = new PeopleSalaryRepository(_connectionString);  // Cria uma nova instância do repositório
        }

        /// <summary>
        /// Adiciona um novo salário para uma pessoa.
        /// </summary>
        /// <param name="model">O modelo de salário da pessoa a ser adicionado.</param>
        /// <returns>Retorna verdadeiro se o salário for adicionado com sucesso; caso contrário, falso.</returns>
        public async Task<bool> AddPeopleSalaryAsync(PeopleSalaryModel model)
        {
            return await _peopleSalaryRepository.AddPeopleSalaryAsync(model);  // Chama o método do repositório para adicionar o salário
        }

        /// <summary>
        /// Obtém um salário de pessoa pelo ID.
        /// </summary>
        /// <param name="id">ID do salário da pessoa.</param>
        /// <returns>Retorna o modelo do salário correspondente ao ID fornecido.</returns>
        public async Task<PeopleSalaryModel> GetPeopleSalaryByIDAsync(int id)
        {
            return await _peopleSalaryRepository.GetPeopleSalarByIDAsync(id);  // Chama o método do repositório para obter o salário pelo ID
        }

        /// <summary>
        /// Obtém um salário de pessoa pelo nome.
        /// </summary>
        /// <param name="name">Nome da pessoa.</param>
        /// <returns>Retorna o modelo do salário correspondente ao nome fornecido.</returns>
        public async Task<PeopleSalaryModel> GetPeopleSalaryByNameAsync(string name)
        {
            return await _peopleSalaryRepository.GetPeopleSalarByNameAsync(name);  // Chama o método do repositório para obter o salário pelo nome
        }

        /// <summary>
        /// Obtém todos os salários de pessoas com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Retorna uma lista de todos os modelos de salários de pessoas.</returns>
        public async Task<IEnumerable<PeopleSalaryModel>> GetAllPeopleSalaryAsync(int pageNumber, int pageSize)
        {
            return await _peopleSalaryRepository.GetAllPeopleSalarAsync(pageNumber, pageSize);  // Chama o método do repositório para obter todos os salários
        }

        /// <summary>
        /// Obtém a contagem total de salários de pessoas.
        /// </summary>
        /// <returns>Retorna o número total de salários de pessoas.</returns>
        public async Task<int> GetTotalPeopleSalaryCountAsync()
        {
            return await _peopleSalaryRepository.GetTotalCountPeopleSalaryAsync();  // Chama o método do repositório para obter a contagem total
        }

        /// <summary>
        /// Atualiza um salário de pessoa existente.
        /// </summary>
        /// <param name="update">O modelo de salário da pessoa a ser atualizado.</param>
        /// <returns>Retorna verdadeiro se o salário for atualizado com sucesso; caso contrário, falso.</returns>
        public async Task<bool> UpdatePeopleSalaryAsync(PeopleSalaryModel update)
        {
            return await _peopleSalaryRepository.UpdatePeopleSalaryAsync(update);  // Chama o método do repositório para atualizar o salário
        }

        /// <summary>
        /// Remove um salário de pessoa pelo ID.
        /// </summary>
        /// <param name="id">ID do salário a ser removido.</param>
        /// <returns>Retorna verdadeiro se o salário for removido com sucesso; caso contrário, falso.</returns>
        public async Task<bool> RemovePeopleSalaryAsync(int id)
        {
            var servicePeople = new PeopleService();  // Cria uma instância do serviço de pessoas

            var peopleSalary = await _peopleSalaryRepository.GetPeopleSalarByIDAsync(id);  // Obtém o salário da pessoa pelo ID
            var people = await servicePeople.GetPeopleByNameAsync(peopleSalary.Name);  // Obtém a pessoa pelo nome do salário

            var response = await _peopleSalaryRepository.DeletePeopleSalaryAsync(id);  // Remove o salário da pessoa
            var removePeople = await servicePeople.RemovePeopleAsync(people.ID);  // Remove a pessoa correspondente

            return response;  // Retorna o resultado da remoção do salário
        }
    }
}
