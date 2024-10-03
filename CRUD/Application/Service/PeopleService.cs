using CRUD.Domain.Entities.Models;  // Importa o namespace que contém os modelos de entidade
using CRUD.Infrastructure.Repositories;  // Importa o namespace que contém os repositórios
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace CRUD.Application.Service
{
    /// <summary>
    /// Classe de serviço para gerenciar operações relacionadas a pessoas.
    /// </summary>
    public class PeopleService
    {
        private readonly string _connectionString;  // String de conexão com o banco de dados
        private readonly PeopleRepository _peopleRepository;  // Repositório para operações de pessoas

        /// <summary>
        /// Construtor da classe PeopleService.
        /// Inicializa a string de conexão e o repositório.
        /// </summary>
        public PeopleService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;  // Obtém a string de conexão do arquivo de configuração
            _peopleRepository = new PeopleRepository(_connectionString);  // Cria uma nova instância do repositório
        }

        /// <summary>
        /// Adiciona uma nova pessoa.
        /// </summary>
        /// <param name="model">O modelo de pessoa a ser adicionado.</param>
        /// <returns>Retorna verdadeiro se a pessoa for adicionada com sucesso; caso contrário, falso.</returns>
        public async Task<bool> AddPeopleAsync(People model)
        {
            var servicePeopleSalary = new PeopleSalaryService();  // Cria uma instância do serviço de salários de pessoas
            var serviceCargo = new CargoService();  // Cria uma instância do serviço de cargos

            // Obtém o modelo de cargo correspondente ao ID do cargo da pessoa
            var cargoModel = await serviceCargo.GetCargoByIDAsync(model.IDCargo);
            // Cria um novo modelo de salário para a pessoa com base nos dados obtidos
            var peopleSalaryModel = new PeopleSalaryModel(model.ID, model.Nome, cargoModel.Salary);

            // Adiciona a pessoa no repositório e obtém a resposta
            var response = await _peopleRepository.AddPeopleAsync(model);
            // Adiciona o salário da pessoa usando o serviço de salários
            var resposePeopleSalary = await servicePeopleSalary.AddPeopleSalaryAsync(peopleSalaryModel);

            return response;  // Retorna a resposta da adição da pessoa
        }

        /// <summary>
        /// Obtém uma pessoa pelo ID.
        /// </summary>
        /// <param name="id">ID da pessoa.</param>
        /// <returns>Retorna o modelo da pessoa correspondente ao ID fornecido.</returns>
        public async Task<People> GetPeopleByIDAsync(int id)
        {
            // Chama o método do repositório para obter a pessoa pelo ID
            var response = await _peopleRepository.GetPeopleByIDAsync(id);
            return response;  // Retorna a pessoa obtida
        }

        /// <summary>
        /// Obtém uma pessoa pelo nome.
        /// </summary>
        /// <param name="name">Nome da pessoa.</param>
        /// <returns>Retorna o modelo da pessoa correspondente ao nome fornecido.</returns>
        public async Task<People> GetPeopleByNameAsync(string name)
        {
            // Chama o método do repositório para obter a pessoa pelo nome
            var response = await _peopleRepository.GetPeopleByNameAsync(name);
            return response;  // Retorna a pessoa obtida
        }

        /// <summary>
        /// Obtém todas as pessoas.
        /// </summary>
        /// <returns>Retorna uma lista de todos os modelos de pessoas.</returns>
        public async Task<IEnumerable<People>> GetAllPeopleAsync()
        {
            // Chama o método do repositório para obter todas as pessoas
            var response = await _peopleRepository.GetAllPeopleAsync();
            return response;  // Retorna a lista de pessoas
        }

        /// <summary>
        /// Atualiza uma pessoa existente.
        /// </summary>
        /// <param name="update">O modelo de pessoa a ser atualizado.</param>
        /// <returns>Retorna verdadeiro se a pessoa for atualizada com sucesso; caso contrário, falso.</returns>
        public async Task<bool> UpdatePeopleAsync(People update)
        {
            var servicePeopleSalary = new PeopleSalaryService();  // Cria uma instância do serviço de salários de pessoas
            var serviceCargo = new CargoService();  // Cria uma instância do serviço de cargos

            // Obtém o modelo de cargo correspondente ao ID do cargo da pessoa
            var cargoModel = await serviceCargo.GetCargoByIDAsync(update.IDCargo);
            // Obtém o modelo de salário da pessoa a ser atualizada
            var peopleSalaryModel = await servicePeopleSalary.GetPeopleSalaryByIDAsync(update.ID);

            // Atualiza os dados do modelo de salário com os novos dados da pessoa
            peopleSalaryModel.Name = update.Nome;
            peopleSalaryModel.Salary = cargoModel.Salary;

            // Atualiza a pessoa no repositório e obtém a resposta
            var response = await _peopleRepository.UpdatePeopleAsync(update);
            // Atualiza o salário da pessoa usando o serviço de salários
            var responsePeopleSalary = await servicePeopleSalary.UpdatePeopleSalaryAsync(peopleSalaryModel);

            return response;  // Retorna a resposta da atualização da pessoa
        }

        /// <summary>
        /// Remove uma pessoa pelo ID.
        /// </summary>
        /// <param name="id">ID da pessoa a ser removida.</param>
        /// <returns>Retorna verdadeiro se a pessoa for removida com sucesso; caso contrário, falso.</returns>
        public async Task<bool> RemovePeopleAsync(int id)
        {
            // Chama o método do repositório para remover a pessoa pelo ID
            var response = await _peopleRepository.RemovePeopleAsync(id);
            return response;  // Retorna a resposta da remoção da pessoa
        }
    }
}
