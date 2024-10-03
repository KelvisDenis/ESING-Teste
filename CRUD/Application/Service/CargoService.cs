using CRUD.Domain.Entities.Models;  // Importa o namespace que contém os modelos de entidade
using CRUD.Infrastructure.Repositories;  // Importa o namespace que contém os repositórios
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace CRUD.Application.Service
{
    /// <summary>
    /// Classe de serviço para gerenciar operações relacionadas ao Cargo.
    /// </summary>
    public class CargoService
    {
        private readonly string _connectionString;  // String de conexão com o banco de dados
        private readonly CargoRepository _cargoRepository;  // Repositório para operações de Cargo

        /// <summary>
        /// Construtor da classe CargoService.
        /// Inicializa a string de conexão e o repositório.
        /// </summary>
        public CargoService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;  // Obtém a string de conexão do arquivo de configuração
            _cargoRepository = new CargoRepository(_connectionString);  // Cria uma nova instância do repositório
        }

        /// <summary>
        /// Adiciona um novo cargo.
        /// </summary>
        /// <param name="model">O modelo do cargo a ser adicionado.</param>
        /// <returns>Retorna verdadeiro se o cargo for adicionado com sucesso; caso contrário, falso.</returns>
        public async Task<bool> AddCargoAsync(CargoModel model)
        {
            return await _cargoRepository.AddCargoAsync(model);  // Chama o método do repositório para adicionar o cargo
        }

        /// <summary>
        /// Obtém um cargo pelo ID.
        /// </summary>
        /// <param name="id">ID do cargo.</param>
        /// <returns>Retorna o modelo do cargo correspondente ao ID fornecido.</returns>
        public async Task<CargoModel> GetCargoByIDAsync(int id)
        {
            return await _cargoRepository.GetCargoByIDAsync(id);  // Chama o método do repositório para obter o cargo pelo ID
        }

        /// <summary>
        /// Obtém um cargo pelo nome.
        /// </summary>
        /// <param name="name">Nome do cargo.</param>
        /// <returns>Retorna o modelo do cargo correspondente ao nome fornecido.</returns>
        public async Task<CargoModel> GetCargoByNameAsync(string name)
        {
            return await _cargoRepository.GetCargoByNameAsync(name);  // Chama o método do repositório para obter o cargo pelo nome
        }

        /// <summary>
        /// Obtém todos os cargos.
        /// </summary>
        /// <returns>Retorna uma lista de todos os modelos de cargos.</returns>
        public async Task<IEnumerable<CargoModel>> GetAllCargoAsync()
        {
            return await _cargoRepository.GetAllCargoAsync();  // Chama o método do repositório para obter todos os cargos
        }

        /// <summary>
        /// Atualiza um cargo existente.
        /// </summary>
        /// <param name="update">O modelo do cargo a ser atualizado.</param>
        /// <returns>Retorna verdadeiro se o cargo for atualizado com sucesso; caso contrário, falso.</returns>
        public async Task<bool> UpdateCargoAsync(CargoModel update)
        {
            return await _cargoRepository.UpdateCargoAsync(update);  // Chama o método do repositório para atualizar o cargo
        }

        /// <summary>
        /// Remove um cargo pelo ID.
        /// </summary>
        /// <param name="id">ID do cargo a ser removido.</param>
        /// <returns>Retorna verdadeiro se o cargo for removido com sucesso; caso contrário, falso.</returns>
        public async Task<bool> RemoveCargoAsync(int id)
        {
            return await _cargoRepository.RemoveCargoAsync(id);  // Chama o método do repositório para remover o cargo
        }
    }
}
