using CRUD.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using Npgsql;

namespace CRUD.Infrastructure.Repositories
{
    public class PeopleRepository
    {
        
            private readonly string _connectionString;

            public PeopleRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            public async Task<People> GetPeopleByIDAsync(int id)
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var command = new NpgsqlCommand("SELECT * FROM public.\"Pessoa\" WHERE \"ID\" = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new People
                                {
                                    ID = (int)reader["ID"],
                                    Nome = (string)reader["Nome"],
                                    CEP = (string)reader["CEP"],
                                    Pais = (string)reader["Pais"],
                                    Cidade= (string)reader["Cidade"],
                                    DataNascimento = (DateTime)reader["Data_Nascimento"],
                                    Email= (string)reader["Email"],
                                    Endereco= (string)reader["Endereco"],
                                    Telefone = (string)reader["Telefone"],
                                    Usuario = (string)reader["Usuario"],
                                    IDCargo = (int)reader["Cargo_ID"],

                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return null;
            }

            public async Task<People> GetPeopleByNameAsync(string name)
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var command = new NpgsqlCommand("SELECT * FROM public.\"Pessoa\"  WHERE \"Nome\" = @Nome", connection);
                    command.Parameters.AddWithValue("@Nome", name);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new People
                                {
                                    ID = (int)reader["ID"],
                                    Nome = (string)reader["Nome"],
                                    CEP = (string)reader["CEP"],
                                    Pais = (string)reader["Pais"],
                                    Cidade = (string)reader["Cidade"],
                                    DataNascimento = (DateTime)reader["Data_Nascimento"],
                                    Email = (string)reader["Email"],
                                    Endereco = (string)reader["Endereco"],
                                    Telefone = (string)reader["Telefone"],
                                    Usuario = (string)reader["Usuario"],
                                    IDCargo = (int)reader["Cargo_ID"],
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return null;
            }
            public async Task<IEnumerable<People>> GetAllPeopleAsync()
            {
                var peoples = new List<People>();
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var command = new NpgsqlCommand("SELECT * FROM public.\"Pessoa\"", connection);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var people = new People
                                {
                                    ID = (int)reader["ID"],
                                    Nome = (string)reader["Nome"],
                                    CEP = (string)reader["CEP"],
                                    Pais = (string)reader["Pais"],
                                    Cidade = (string)reader["Cidade"],
                                    DataNascimento = (DateTime)reader["Data_Nascimento"],
                                    Email = (string)reader["Email"],
                                    Endereco = (string)reader["Endereco"],
                                    Telefone = (string)reader["Telefone"],
                                    Usuario = (string)reader["Usuario"],
                                    IDCargo = (int)reader["Cargo_ID"],
                                };
                                peoples.Add(people);
                            }
                        return peoples;
                        }
                   
                }
                catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            return null;
            }

            public async Task<bool> AddPeopleAsync(People model)
                {
                    using (var connection = new NpgsqlConnection(_connectionString))
                    {
                        var command = new NpgsqlCommand("INSERT INTO public.\"Pessoa\" (\"Nome\", \"Cidade\", \"Email\"" +
                            ", \"CEP\", \"Endereco\", \"Pais\", \"Usuario\", \"Data_Nascimento\", \"Telefone\", \"Cargo_ID\" )" +
                            " VALUES (@Nome, @Cidade, @Email, @CEP, @Endereco, @Pais, @Usuario, @Data_Nascimento, @Telefone, @Cargo_ID)", connection);
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

            public async Task<bool> UpdatePeopleAsync(People model)
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var command = new NpgsqlCommand(
                    "UPDATE public.\"Pessoa\" SET " +
                    "\"Nome\" = @Nome, " +
                    "\"Cidade\" = @Cidade, " +
                    "\"Email\" = @Email, " +
                    "\"CEP\" = @CEP, " +
                    "\"Endereco\" = @Endereco, " +
                    "\"Pais\" = @Pais, " +
                    "\"Usuario\" = @Usuario, " +
                    "\"Data_Nascimento\" = @Data_Nascimento, " +
                    "\"Telefone\" = @Telefone, " +
                    "\"Cargo_ID\" = @Cargo_ID " +
                    "WHERE \"ID\" = @ID", connection);

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

            public async Task<bool> RemovePeopleAsync(int id)
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var command = new NpgsqlCommand("DELETE FROM  public.\"Pessoa\" WHERE \"ID\" = @ID", connection);
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