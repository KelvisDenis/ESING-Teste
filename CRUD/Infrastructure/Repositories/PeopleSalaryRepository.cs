using CRUD.Domain.Entities.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUD.Infrastructure.Repositories
{
    public class PeopleSalaryRepository
    {

        private readonly string _connectionString;

        public PeopleSalaryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddPeopleSalaryAsync(PeopleSalaryModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("INSERT INTO public.\"Pessoa_Salario\" (\"Nome\", \"Salario\")" +
                    " VALUES (@Nome, @Salario)", connection);
                command.Parameters.AddWithValue("@Nome", model.Name);
                command.Parameters.AddWithValue("@Salario", model.Salary);
                

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public async Task<PeopleSalaryModel> GetPeopleSalarByIDAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"Pessoa_Salario\" WHERE \"ID\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new PeopleSalaryModel
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Nome"],
                                Salary = (int)reader["Salario"],

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
        public async Task<PeopleSalaryModel> GetPeopleSalarByNameAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"Pessoa_Salario\" WHERE \"Nome\" = @Nome", connection);
                command.Parameters.AddWithValue("@Nome", name);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new PeopleSalaryModel
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Nome"],
                                Salary = (int)reader["Salario"],

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
        public async Task<IEnumerable<PeopleSalaryModel>> GetAllPeopleSalarAsync(int pageNumber, int pageSize)
        {
            var peoples = new List<PeopleSalaryModel>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                // Adiciona a lógica de paginação usando OFFSET e LIMIT
                var command = new NpgsqlCommand(
                    "SELECT * FROM public.\"Pessoa_Salario\" " +
                    "ORDER BY \"ID\" " +
                    "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", connection);

                // Calcula o deslocamento baseado na página e no tamanho da página
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var people = new PeopleSalaryModel
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Nome"],
                                Salary = (int)reader["Salario"],
                            };
                            peoples.Add(people);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return peoples;
        }
        public async Task<bool> UpdatePeopleSalaryAsync(PeopleSalaryModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand(
                "UPDATE public.\"Pessoa_Salario\" SET \"Nome\" = @Nome, \"Salario\" = @Salario, WHERE \"ID\" = @ID", connection);
                command.Parameters.AddWithValue("@Nome", model.Name);
                command.Parameters.AddWithValue("@Salario", model.Salary);
                command.Parameters.AddWithValue("@ID", model.ID);
               

                try
                {
                    await connection.OpenAsync();
                    var rows = await command.ExecuteNonQueryAsync();
                    return rows > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public async Task<bool> RemovePeopleSalaryAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("DELETE FROM  public.\"Pessoa_Salario\" WHERE \"ID\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    await connection.OpenAsync();
                    var rows = await command.ExecuteNonQueryAsync();
                    return rows > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public async Task<int> GetTotalCountPeopleSalaryAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"Pessoa_Salario\"", connection);
                try
                {
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    if (result is long count)  return (int)count;
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