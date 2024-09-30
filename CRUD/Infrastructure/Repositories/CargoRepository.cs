using CRUD.Domain.Entities.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUD.Infrastructure.Repositories
{
    public class CargoRepository
    {
        private readonly string _connectionString;

        public CargoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddCargoAsync(CargoModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("INSERT INTO public.\"Cargo\" (\"ID\", \"Nome\", \"Salario\")" +
                    " VALUES (@ID, @Nome, @Salario, )", connection);
                command.Parameters.AddWithValue("@ID", model.ID);
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
        public async Task<CargoModel> GetCargoByIDAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"Cargo\" WHERE \"ID\" = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new CargoModel
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
        public async Task<CargoModel> GetCargoByNameAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"Cargo\" WHERE \"Nome\" = @Nome", connection);
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
        public async Task<IEnumerable<CargoModel>> GetAllCargoAsync()
        {
            var cargos = new List<CargoModel>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM public.\"Cargo\"", connection);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cargo = new CargoModel
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Nome"],
                                Salary = (int)reader["Salario"],

                            };
                            cargos.Add(cargo);
                        }
                        return cargos;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }
        public async Task<bool> UpdateCargoAsync(CargoModel model)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand(
                "UPDATE public.\"Cargo\" SET \"Nome\" = @Nome, \"Salario\" = @Salario, WHERE \"ID\" = @ID", connection);
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
        public async Task<bool> RemoveCargoAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("DELETE FROM  public.\"Cargo\" WHERE \"ID\" = @ID", connection);
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

    }
}