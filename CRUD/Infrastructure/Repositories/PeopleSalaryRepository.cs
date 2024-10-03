using CRUD.Domain.Entities.Models; // Importa o modelo PeopleSalaryModel do namespace correspondente
using Npgsql; // Importa o namespace Npgsql para interação com o banco de dados PostgreSQL
using System; // Importa o namespace System para tipos básicos e funcionalidades gerais
using System.Collections.Generic; // Importa o namespace para usar coleções genéricas
using System.Linq; // Importa o namespace para operações de LINQ
using System.Threading.Tasks; // Importa o namespace para suportar operações assíncronas

namespace CRUD.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para gerenciar operações relacionadas a salários de pessoas.
    /// </summary>
    public class PeopleSalaryRepository
    {
        private readonly string _connectionString; // Armazena a string de conexão com o banco de dados

        /// <summary>
        /// Construtor que inicializa o repositório com a string de conexão fornecida.
        /// </summary>
        /// <param name="connectionString">A string de conexão para o banco de dados.</param>
        public PeopleSalaryRepository(string connectionString)
        {
            _connectionString = connectionString; // Inicializa a variável de conexão
        }

        /// <summary>
        /// Método assíncrono que adiciona um novo registro de salário de uma pessoa ao banco de dados.
        /// </summary>
        /// <param name="model">O modelo de salário da pessoa a ser adicionado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> AddPeopleSalaryAsync(PeopleSalaryModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para inserir um novo registro de salário
                var command = new NpgsqlCommand("INSERT INTO public.\"pessoa_salario\" (\"nome\", \"salario\")" +
                    " VALUES (@Nome, @Salario)", connection);
                command.Parameters.AddWithValue("@Nome", model.Name); // Adiciona o parâmetro do nome
                command.Parameters.AddWithValue("@Salario", model.Salary); // Adiciona o parâmetro do salário

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    await command.ExecuteNonQueryAsync(); // Executa o comando
                    return true; // Retorna verdadeiro se a inserção foi bem-sucedida
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                    return false; // Retorna falso se ocorrer um erro
                }
            }
        }

        /// <summary>
        /// Método assíncrono que busca um registro de salário de pessoa pelo ID.
        /// </summary>
        /// <param name="id">O ID da pessoa cujo salário será buscado.</param>
        /// <returns>Um objeto PeopleSalaryModel representando o salário da pessoa, ou null se não encontrado.</returns>
        public async Task<PeopleSalaryModel> GetPeopleSalarByIDAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar um registro pelo ID
                var command = new NpgsqlCommand("SELECT * FROM public.\"pessoa_salario\" WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id); // Adiciona o parâmetro ID ao comando

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém o leitor de dados
                    {
                        if (await reader.ReadAsync()) // Lê a próxima linha de resultados
                        {
                            return new PeopleSalaryModel // Retorna o modelo preenchido com os dados do banco
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                        }
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                    return null; // Retorna null se ocorrer um erro
                }
            }
            return null; // Retorna null se não encontrar o registro
        }

        /// <summary>
        /// Método assíncrono que busca um registro de salário de pessoa pelo nome.
        /// </summary>
        /// <param name="name">O nome da pessoa cujo salário será buscado.</param>
        /// <returns>Um objeto PeopleSalaryModel representando o salário da pessoa, ou null se não encontrado.</returns>
        public async Task<PeopleSalaryModel> GetPeopleSalarByNameAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar um registro pelo nome
                var command = new NpgsqlCommand("SELECT * FROM public.\"pessoa_salario\" WHERE \"nome\" = @Nome", connection);
                command.Parameters.AddWithValue("@Nome", name); // Adiciona o parâmetro nome ao comando

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém o leitor de dados
                    {
                        if (await reader.ReadAsync()) // Lê a próxima linha de resultados
                        {
                            return new PeopleSalaryModel // Retorna o modelo preenchido com os dados do banco
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                        }
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                    return null; // Retorna null se ocorrer um erro
                }
            }
            return null; // Retorna null se não encontrar o registro
        }

        /// <summary>
        /// Método assíncrono que busca os registros por nome de pessoa_salario, com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página a ser retornada.</param>  
        /// <param name="pageSize">Número de registros por página.</param>
        /// <returns>Uma lista de objetos PeopleSalaryModel representando os salários das pessoas.</returns>
        public async Task<IEnumerable<PeopleSalaryModel>> GetAllPeopleSalarByNameAsync(int pageNumber, int pageSize , string name)
        {
            var peoples = new List<PeopleSalaryModel>(); // Cria uma lista para armazenar os registros
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar todos os registros com paginação
                var command = new NpgsqlCommand(
                    "SELECT * FROM public.\"pessoa_salario\" " +
                    "WHERE \"nome\" = @Nome " +
                    "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", connection);

                // Calcula o deslocamento baseado na página e no tamanho da página
                command.Parameters.AddWithValue("@Nome", name); // Adiciona o parâmetro de nome ao comando
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize); // Adiciona o parâmetro de deslocamento
                command.Parameters.AddWithValue("@PageSize", pageSize); // Adiciona o parâmetro de tamanho da página

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém o leitor de dados
                    {
                        while (await reader.ReadAsync()) // Lê as linhas de resultados
                        {
                            var people = new PeopleSalaryModel // Cria um novo objeto para cada registro encontrado
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                            peoples.Add(people); // Adiciona o objeto à lista
                        }
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return peoples; // Retorna a lista de registros com o nome selecionado
        }

        /// <summary>
        /// Método assíncrono que busca todos os registros de salários de pessoas, com paginação.
        /// </summary>
        /// <param name="pageNumber">Número da página a ser retornada.</param>
        /// <param name="pageSize">Número de registros por página.</param>
        /// <returns>Uma lista de objetos PeopleSalaryModel representando os salários das pessoas.</returns>
        public async Task<IEnumerable<PeopleSalaryModel>> GetAllPeopleSalarAsync(int pageNumber, int pageSize)
        {
            var peoples = new List<PeopleSalaryModel>(); // Cria uma lista para armazenar os registros
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar todos os registros com paginação
                var command = new NpgsqlCommand(
                    "SELECT * FROM public.\"pessoa_salario\" " +
                    "ORDER BY \"id\" " +
                    "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", connection);

                // Calcula o deslocamento baseado na página e no tamanho da página
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize); // Adiciona o parâmetro de deslocamento
                command.Parameters.AddWithValue("@PageSize", pageSize); // Adiciona o parâmetro de tamanho da página

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém o leitor de dados
                    {
                        while (await reader.ReadAsync()) // Lê as linhas de resultados
                        {
                            var people = new PeopleSalaryModel // Cria um novo objeto para cada registro encontrado
                            {
                                ID = (int)reader["id"],
                                Name = (string)reader["nome"],
                                Salary = (int)reader["salario"],
                            };
                            peoples.Add(people); // Adiciona o objeto à lista
                        }
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return peoples; // Retorna a lista de registros
        }

        /// <summary>
        /// Método assíncrono que atualiza um registro de salário de pessoa existente no banco de dados.
        /// </summary>
        /// <param name="model">O modelo de salário da pessoa a ser atualizado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> UpdatePeopleSalaryAsync(PeopleSalaryModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para atualizar um registro existente
                var command = new NpgsqlCommand(
                "UPDATE public.\"pessoa_salario\" SET \"nome\" = @Nome, \"salario\" = @Salario WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@Nome", model.Name); // Adiciona o parâmetro do nome
                command.Parameters.AddWithValue("@Salario", model.Salary); // Adiciona o parâmetro do salário
                command.Parameters.AddWithValue("@ID", model.ID); // Adiciona o parâmetro do ID

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    await command.ExecuteNonQueryAsync(); // Executa o comando de atualização
                    return true; // Retorna verdadeiro se a atualização foi bem-sucedida
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                    return false; // Retorna falso se ocorrer um erro
                }
            }
        }

        /// <summary>
        /// Método assíncrono que exclui um registro de salário de pessoa pelo ID.
        /// </summary>
        /// <param name="id">O ID da pessoa cujo registro de salário será excluído.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> DeletePeopleSalaryAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para excluir um registro pelo ID
                var command = new NpgsqlCommand("DELETE FROM public.\"pessoa_salario\" WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id); // Adiciona o parâmetro ID ao comando

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    await command.ExecuteNonQueryAsync(); // Executa o comando de exclusão
                    return true; // Retorna verdadeiro se a exclusão foi bem-sucedida
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                    return false; // Retorna falso se ocorrer um erro
                }
            }
        }

        /// <summary>
        /// Método assíncrono que  busca o numero total de pessoas no banco de dados.
        /// </summary>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<int> GetTotalCountPeopleSalaryAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"pessoa_salario\"", connection);
                try
                {
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    if (result is long count) return (int)count;
                    return 0;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
    }
}
