using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUD.Infrastructure.Data
{
    public class CreateTableDB
    {
        private string connectionString;

        public CreateTableDB()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
        }
        public async Task CriarTabelasSeNaoExistirem()
        {

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                var criarTabelasSql = @"
                    CREATE TABLE IF NOT EXISTS Pessoa (
                        ID SERIAL PRIMARY KEY,
                        Nome TEXT NOT NULL,
                        Cidade TEXT NOT NULL,
                        Email TEXT,
                        CEP TEXT,
                        Endereco TEXT,
                        Pais TEXT,
                        Usuario TEXT,
                        Telefone TEXT,
                        Data_Nascimento DATE,
                        Cargo_ID INT
                    );

                    CREATE TABLE IF NOT EXISTS Cargo (
                        ID SERIAL PRIMARY KEY,
                        Nome TEXT NOT NULL,
                        Salario INT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Pessoa_Salario (
                        ID SERIAL PRIMARY KEY,
                        Nome TEXT,
                        Salario INT
                        
                    );
                ";

                using (var cmd = new NpgsqlCommand(criarTabelasSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                var checkDataSql = "SELECT COUNT(*) FROM Pessoa_Salario";
                using (var cmd = new NpgsqlCommand(checkDataSql, conn))
                {
                    var count = (long)await cmd.ExecuteScalarAsync();

                    if (count == 0)
                    {
                        await ImportarDadosCSV(conn);
                    }
                }
            }
        }

        public async Task ImportarDadosCSV(NpgsqlConnection conn)
        {
            string pastaData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Data");
            pastaData = Path.GetFullPath(pastaData);

            var arquivoPessoa = Path.Combine(pastaData, "Pessoa.csv");
            var arquivoCargo = Path.Combine(pastaData, "Cargo.csv");

            try
            {
                // Importar dados da tabela Pessoa
                using (var reader = new StreamReader(arquivoPessoa))
                {
                    bool isFirstLine = true;
                    while (!reader.EndOfStream)
                    {
                        var linha = await reader.ReadLineAsync();

                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }
                        var dados = linha.Split(','); // Ajuste conforme a formatação do seu CSV

                        var insertSql = "INSERT INTO Pessoa(Nome, Cidade, Email, CEP, Endereco, Pais, Usuario, Telefone, Data_Nascimento, Cargo_ID) VALUES(@Nome, @Cidade, @Email, @CEP, @Endereco, @Pais, @Usuario, @Telefone, @Data_Nascimento, @Cargo_ID)";

                        using (var cmd = new NpgsqlCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("Nome", dados[1]);
                            cmd.Parameters.AddWithValue("Cidade", dados[2]);
                            cmd.Parameters.AddWithValue("Email", dados[3]);
                            cmd.Parameters.AddWithValue("CEP", dados[4]);
                            cmd.Parameters.AddWithValue("Endereco", dados[5]);
                            cmd.Parameters.AddWithValue("Pais", dados[6]);
                            cmd.Parameters.AddWithValue("Usuario", dados[7]);
                            cmd.Parameters.AddWithValue("Telefone", dados[8]);
                            DateTime dataNascimento;
                            string formatoData = "yyyy/MM/dd"; // Ajuste conforme necessário
                            if (!DateTime.TryParseExact(dados[9], formatoData, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                            {
                                Console.WriteLine($"Data de nascimento inválida na linha: {linha}");
                                continue; // Ignorar linha
                            }
                            cmd.Parameters.AddWithValue("Data_Nascimento", dataNascimento);
                            cmd.Parameters.AddWithValue("Cargo_ID", int.Parse(dados[10]));


                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                Console.WriteLine("Dados importados para a tabela Pessoa com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar dados da tabela Pessoa: {ex.Message}");
            }

            try
            {
                // Importar dados da tabela Cargo
                using (var reader = new StreamReader(arquivoCargo))
                {
                    bool isFirstLine = true;

                    while (!reader.EndOfStream)
                    {
                        var linha = await reader.ReadLineAsync();
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }
                        var dados = linha.Split(','); // Ajuste conforme a formatação do seu CSV

                        var insertSql = "INSERT INTO Cargo(Nome, Salario) VALUES(@Nome, @Salario)";

                        using (var cmd = new NpgsqlCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("Nome", dados[1]);
                            cmd.Parameters.AddWithValue("Salario", decimal.Parse(dados[2]));

                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                Console.WriteLine("Dados importados para a tabela Cargo com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar dados da tabela Cargo: {ex.Message}");
            }

            var preencherPessoaSalarioSql = @"
                INSERT INTO pessoa_salario (nome, salario)
                SELECT p.nome, c.salario
                FROM pessoa p
                JOIN cargo c ON p.cargo_id = c.id;
            ";

            try
            {
                using (var cmd = new NpgsqlCommand(preencherPessoaSalarioSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine("Dados da tabela pessoa_salario preenchidos com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher tabela pessoa_salario: {ex.Message}");
            }
        }

    }
}
