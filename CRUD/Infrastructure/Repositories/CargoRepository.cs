using CRUD.Domain.Entities.Models; // Importa o namespace que contém o modelo CargoModel
using Npgsql; // Importa Npgsql para operações de banco de dados PostgreSQL
using System; // Importa funcionalidades básicas do sistema
using System.Collections.Generic; // Importa coleções genéricas
using System.Threading.Tasks; // Importa suporte para programação assíncrona

namespace CRUD.Infrastructure.Repositories
{
    /// <summary>
    /// Classe CargoRepository que gerencia as operações de acesso a dados para a entidade Cargo.
    /// </summary>
    public class CargoRepository
    {
        private readonly string _connectionString; // String de conexão para acesso ao banco de dados

        /// <summary>
        /// Construtor que inicializa a string de conexão.
        /// </summary>
        /// <param name="connectionString">A string de conexão para o banco de dados.</param>
        public CargoRepository(string connectionString)
        {
            _connectionString = connectionString; // Inicializa a string de conexão
        }

        /// <summary>
        /// Adiciona um novo Cargo ao banco de dados.
        /// </summary>
        /// <param name="model">O modelo Cargo a ser adicionado.</param>
        /// <returns>True se o cargo foi adicionado com sucesso; caso contrário, False.</returns>
        public async Task<bool> AddCargoAsync(CargoModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão
            {
                // Comando SQL para inserir um novo registro na tabela cargo
                var command = new NpgsqlCommand("INSERT INTO public.\"cargo\" (\"id\", \"nome\", \"salario\") " +
                    "VALUES (@ID, @Nome, @Salario)", connection);

                // Adiciona parâmetros para prevenir injeção de SQL
                command.Parameters.AddWithValue("@ID", model.ID);
                command.Parameters.AddWithValue("@Nome", model.Name);
                command.Parameters.AddWithValue("@Salario", model.Salary);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    await command.ExecuteNonQueryAsync(); // Executa o comando
                    return true; // Retorna true se a operação foi bem-sucedida
                }
                catch (Exception ex) // Captura quaisquer exceções
                {
                    Console.WriteLine(ex.Message); // Loga a mensagem de erro
                    return false; // Retorna false se houve um erro
                }
            }
        }

        /// <summary>
        /// Obtém um Cargo pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cargo a ser obtido.</param>
        /// <returns>Um objeto CargoModel se encontrado; caso contrário, null.</returns>
        public async Task<CargoModel> GetCargoByIDAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"cargo\" WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o leitor
                    {
                        if (await reader.ReadAsync()) // Lê os dados
                        {
                            return new CargoModel // Cria e retorna uma instância de CargoModel
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Loga a mensagem de erro
                    return null; // Retorna null se houve um erro
                }
            }
            return null; // Retorna null se nenhum dado for encontrado
        }

        /// <summary>
        /// Obtém um Cargo pelo seu nome.
        /// </summary>
        /// <param name="name">O nome do cargo a ser obtido.</param>
        /// <returns>Um objeto CargoModel se encontrado; caso contrário, null.</returns>
        public async Task<CargoModel> GetCargoByNameAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"cargo\" WHERE \"nome\" = @Nome", connection);
                command.Parameters.AddWithValue("@Nome", name);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new CargoModel
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtém todos os Cargos do banco de dados.
        /// </summary>
        /// <returns>Uma coleção de objetos CargoModel.</returns>
        public async Task<IEnumerable<CargoModel>> GetAllCargoAsync()
        {
            var cargos = new List<CargoModel>(); // Lista para armazenar os cargos
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"cargo\"", connection);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) // Lê os dados enquanto houver registros
                        {
                            var cargo = new CargoModel // Cria uma instância de CargoModel
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                            cargos.Add(cargo); // Adiciona o cargo à lista
                        }
                        return cargos; // Retorna a lista de cargos
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Loga a mensagem de erro
                }
            }
            return null; // Retorna null se houve um erro
        }

        /// <summary>
        /// Atualiza um Cargo existente no banco de dados.
        /// </summary>
        /// <param name="model">O modelo Cargo com as informações atualizadas.</param>
        /// <returns>True se o cargo foi atualizado com sucesso; caso contrário, False.</returns>
        public async Task<bool> UpdateCargoAsync(CargoModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                // Comando SQL para atualizar um registro existente na tabela cargo
                var command = new NpgsqlCommand(
                "UPDATE public.\"cargo\" SET \"nome\" = @Nome, \"salario\" = @Salario WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@Nome", model.Name);
                command.Parameters.AddWithValue("@Salario", model.Salary);
                command.Parameters.AddWithValue("@ID", model.ID);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    var rows = await command.ExecuteNonQueryAsync(); // Executa o comando
                    return rows > 0; // Retorna true se houve alterações
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Loga a mensagem de erro
                    return false; // Retorna false se houve um erro
                }
            }
        }

        /// <summary>
        /// Remove um Cargo do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cargo a ser removido.</param>
        /// <returns>True se o cargo foi removido com sucesso; caso contrário, False.</returns>
        public async Task<bool> RemoveCargoAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("DELETE FROM public.\"cargo\" WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    var rows = await command.ExecuteNonQueryAsync(); // Executa o comando
                    return rows > 0; // Retorna true se o cargo foi removido
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Loga a mensagem de erro
                    return false; // Retorna false se houve um erro
                }
            }
        }
    }
}
