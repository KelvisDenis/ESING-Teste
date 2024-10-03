using CRUD.Domain.Entities.Models; // Importa o namespace que contém as classes de modelo
using System; // Importa o namespace base do .NET
using System.Collections.Generic; // Importa o namespace para coleções genéricas
using System.Linq; // Importa o namespace para LINQ
using System.Web; // Importa o namespace para aplicações ASP.NET
using System.Threading.Tasks; // Importa o namespace para tarefas assíncronas
using System.Data.Entity; // Importa o namespace para o Entity Framework
using System.Data.SqlClient; // Importa o namespace para o SQL Server
using Npgsql; // Importa o namespace para o Npgsql, um driver de acesso a dados PostgreSQL

namespace CRUD.Infrastructure.Repositories // Define o namespace para o repositório
{
    /// <summary>
    /// Classe que gerencia o acesso a dados relacionados a pessoas.
    /// </summary>
    public class PeopleRepository
    {
        private readonly string _connectionString; // Armazena a string de conexão para o banco de dados

        /// <summary>
        /// Construtor que inicializa o repositório com a string de conexão fornecida.
        /// </summary>
        /// <param name="connectionString">A string de conexão para o banco de dados.</param>
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString; // Atribui a string de conexão ao campo privado
        }

        /// <summary>
        /// Método assíncrono que busca uma pessoa pelo ID.
        /// </summary>
        /// <param name="id">O ID da pessoa a ser buscada.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo a pessoa encontrada.</returns>
        public async Task<People> GetPeopleByIDAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar a pessoa pelo ID
                var command = new NpgsqlCommand("SELECT * FROM public.\"pessoa\" WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id); // Adiciona o parâmetro ID ao comando

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém um leitor
                    {
                        if (await reader.ReadAsync()) // Lê o resultado
                        {
                            // Retorna uma nova instância de People preenchida com os dados da leitura
                            return new People
                            {
                                ID = (int)reader["id"],
                                Nome = (string)reader["nome"],
                                CEP = (string)reader["cep"],
                                Pais = (string)reader["pais"],
                                Cidade = (string)reader["cidade"],
                                DataNascimento = (DateTime)reader["data_nascimento"],
                                Email = (string)reader["email"],
                                Endereco = (string)reader["endereco"],
                                Telefone = (string)reader["telefone"],
                                Usuario = (string)reader["usuario"],
                                IDCargo = (int)reader["cargo_id"],
                            };
                        }
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return null; // Retorna null se a pessoa não for encontrada
        }

        /// <summary>
        /// Método assíncrono que busca uma pessoa pelo nome.
        /// </summary>
        /// <param name="name">O nome da pessoa a ser buscada.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo a pessoa encontrada.</returns>
        public async Task<People> GetPeopleByNameAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar a pessoa pelo nome
                var command = new NpgsqlCommand("SELECT * FROM public.\"pessoa\" WHERE \"nome\" = @Nome", connection);
                command.Parameters.AddWithValue("@Nome", name); // Adiciona o parâmetro Nome ao comando

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém um leitor
                    {
                        if (await reader.ReadAsync()) // Lê o resultado
                        {
                            // Retorna uma nova instância de People preenchida com os dados da leitura
                            return new People
                            {
                                ID = (int)reader["id"],
                                Nome = (string)reader["nome"],
                                CEP = (string)reader["cep"],
                                Pais = (string)reader["pais"],
                                Cidade = (string)reader["cidade"],
                                DataNascimento = (DateTime)reader["data_nascimento"],
                                Email = (string)reader["email"],
                                Endereco = (string)reader["endereco"],
                                Telefone = (string)reader["telefone"],
                                Usuario = (string)reader["usuario"],
                                IDCargo = (int)reader["cargo_id"],
                            };
                        }
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return null; // Retorna null se a pessoa não for encontrada
        }

        /// <summary>
        /// Método assíncrono que retorna todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo a lista de pessoas.</returns>
        public async Task<IEnumerable<People>> GetAllPeopleAsync()
        {
            var peoples = new List<People>(); // Inicializa a lista que armazenará as pessoas
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para buscar todas as pessoas
                var command = new NpgsqlCommand("SELECT * FROM public.\"pessoa\"", connection);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    using (var reader = await command.ExecuteReaderAsync()) // Executa o comando e obtém um leitor
                    {
                        while (await reader.ReadAsync()) // Enquanto houver resultados
                        {
                            // Cria uma nova instância de People e a preenche com os dados da leitura
                            var people = new People
                            {
                                ID = (int)reader["id"],
                                Nome = (string)reader["nome"],
                                CEP = (string)reader["cep"],
                                Pais = (string)reader["pais"],
                                Cidade = (string)reader["cidade"],
                                DataNascimento = (DateTime)reader["data_nascimento"],
                                Email = (string)reader["email"],
                                Endereco = (string)reader["endereco"],
                                Telefone = (string)reader["telefone"],
                                Usuario = (string)reader["usuario"],
                                IDCargo = (int)reader["cargo_id"],
                            };
                            peoples.Add(people); // Adiciona a pessoa à lista
                        }
                        return peoples; // Retorna a lista de pessoas
                    }
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return null; // Retorna null se não houver pessoas cadastradas
        }

        /// <summary>
        /// Método assíncrono que adiciona uma nova pessoa ao banco de dados.
        /// </summary>
        /// <param name="model">O modelo da pessoa a ser adicionada.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> AddPeopleAsync(People model)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para inserir uma nova pessoa
                var command = new NpgsqlCommand("INSERT INTO public.\"pessoa\" (\"nome\", \"cidade\", \"email\", \"cep\", \"endereco\", \"pais\", \"usuario\", \"data_nascimento\", \"telefone\", \"cargo_id\") VALUES (@Nome, @Cidade, @Email, @CEP, @Endereco, @Pais, @Usuario, @Data_Nascimento, @Telefone, @Cargo_ID)", connection);
                // Adiciona os parâmetros ao comando
                command.Parameters.AddWithValue("@Nome", model.Nome);
                command.Parameters.AddWithValue("@Cidade", model.Cidade);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@CEP", model.CEP);
                command.Parameters.AddWithValue("@Endereco", model.Endereco);
                command.Parameters.AddWithValue("@Pais", model.Pais);
                command.Parameters.AddWithValue("@Usuario", model.Usuario);
                command.Parameters.AddWithValue("@Data_Nascimento", model.DataNascimento);
                command.Parameters.AddWithValue("@Telefone", model.Telefone);
                command.Parameters.AddWithValue("@Cargo_ID", model.IDCargo);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    var result = await command.ExecuteNonQueryAsync(); // Executa o comando e obtém o número de linhas afetadas
                    return result > 0; // Retorna verdadeiro se pelo menos uma linha foi afetada
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return false; // Retorna falso se a adição falhar
        }

        /// <summary>
        /// Método assíncrono que atualiza uma pessoa existente no banco de dados.
        /// </summary>
        /// <param name="model">O modelo da pessoa a ser atualizado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> UpdatePeopleAsync(People model)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para atualizar uma pessoa existente
                var command = new NpgsqlCommand("UPDATE public.\"pessoa\" SET \"nome\" = @Nome, \"cidade\" = @Cidade, \"email\" = @Email, \"cep\" = @CEP, \"endereco\" = @Endereco, \"pais\" = @Pais, \"usuario\" = @Usuario, \"data_nascimento\" = @Data_Nascimento, \"telefone\" = @Telefone, \"cargo_id\" = @Cargo_ID WHERE \"id\" = @ID", connection);
                // Adiciona os parâmetros ao comando
                command.Parameters.AddWithValue("@ID", model.ID);
                command.Parameters.AddWithValue("@Nome", model.Nome);
                command.Parameters.AddWithValue("@Cidade", model.Cidade);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@CEP", model.CEP);
                command.Parameters.AddWithValue("@Endereco", model.Endereco);
                command.Parameters.AddWithValue("@Pais", model.Pais);
                command.Parameters.AddWithValue("@Usuario", model.Usuario);
                command.Parameters.AddWithValue("@Data_Nascimento", model.DataNascimento);
                command.Parameters.AddWithValue("@Telefone", model.Telefone);
                command.Parameters.AddWithValue("@Cargo_ID", model.IDCargo);

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    var result = await command.ExecuteNonQueryAsync(); // Executa o comando e obtém o número de linhas afetadas
                    return result > 0; // Retorna verdadeiro se pelo menos uma linha foi afetada
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return false; // Retorna falso se a atualização falhar
        }

        /// <summary>
        /// Método assíncrono que remove uma pessoa do banco de dados pelo ID.
        /// </summary>
        /// <param name="id">O ID da pessoa a ser removida.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> RemovePeopleAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString)) // Cria uma nova conexão com o banco de dados
            {
                // Prepara o comando SQL para remover uma pessoa pelo ID
                var command = new NpgsqlCommand("DELETE FROM public.\"pessoa\" WHERE \"id\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id); // Adiciona o parâmetro ID ao comando

                try
                {
                    await connection.OpenAsync(); // Abre a conexão de forma assíncrona
                    var result = await command.ExecuteNonQueryAsync(); // Executa o comando e obtém o número de linhas afetadas
                    return result > 0; // Retorna verdadeiro se pelo menos uma linha foi afetada
                }
                catch (Exception ex) // Captura qualquer exceção que ocorra
                {
                    Console.WriteLine(ex.Message); // Exibe a mensagem de erro no console
                }
            }
            return false; // Retorna falso se a remoção falhar
        }
    }
}
