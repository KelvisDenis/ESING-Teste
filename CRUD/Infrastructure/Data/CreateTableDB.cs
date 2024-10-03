using Npgsql;  // Importa o namespace para conexão com o banco de dados PostgreSQL
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
        private string connectionString; // Armazena a string de conexão com o banco de dados

        // Construtor que inicializa a string de conexão com o banco de dados PostgreSQL
        public CreateTableDB()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
        }

        /// <summary>
        /// Método assíncrono que cria tabelas no banco de dados se não existirem.
        /// Também verifica a tabela Pessoa_Salario e importa dados de arquivos CSV, se necessário.
        /// </summary>
        public async Task CriarTabelasSeNaoExistirem()
        {
            using (var conn = new NpgsqlConnection(connectionString)) // Estabelece uma conexão com o banco de dados
            {
                conn.Open(); // Abre a conexão

                // Comando SQL para criar tabelas
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

                // Executa o comando SQL para criar as tabelas
                using (var cmd = new NpgsqlCommand(criarTabelasSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync(); // Executa o comando de criação
                }

                // Comando SQL para verificar se a tabela Pessoa_Salario possui dados
                var checkDataSql = "SELECT COUNT(*) FROM Pessoa_Salario";
                using (var cmd = new NpgsqlCommand(checkDataSql, conn))
                {
                    var count = (long)await cmd.ExecuteScalarAsync(); // Conta o número de registros

                    // Se não houver dados, importa os dados de arquivos CSV
                    if (count == 0)
                    {
                        await ImportarDadosCSV(conn);
                    }
                }
            }
        }

        /// <summary>
        /// Método assíncrono que importa dados de arquivos CSV para as tabelas Pessoa e Cargo.
        /// </summary>
        public async Task ImportarDadosCSV(NpgsqlConnection conn)
        {
            // Define o caminho para os arquivos CSV
            string pastaData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Data");
            pastaData = Path.GetFullPath(pastaData);

            var arquivoPessoa = Path.Combine(pastaData, "Pessoa.csv");
            var arquivoCargo = Path.Combine(pastaData, "Cargo.csv");

            // Importa dados da tabela Pessoa
            try
            {
                using (var reader = new StreamReader(arquivoPessoa))
                {
                    bool isFirstLine = true; // Variável para ignorar o cabeçalho
                    while (!reader.EndOfStream)
                    {
                        var linha = await reader.ReadLineAsync(); // Lê cada linha do arquivo CSV

                        if (isFirstLine)
                        {
                            isFirstLine = false; // Ignora a primeira linha (cabeçalho)
                            continue;
                        }

                        var dados = linha.Split(','); // Divide os dados da linha

                        // Comando SQL para inserir dados na tabela Pessoa
                        var insertSql = "INSERT INTO Pessoa(Nome, Cidade, Email, CEP, Endereco, Pais, Usuario, Telefone, Data_Nascimento, Cargo_ID) VALUES(@Nome, @Cidade, @Email, @CEP, @Endereco, @Pais, @Usuario, @Telefone, @Data_Nascimento, @Cargo_ID)";

                        using (var cmd = new NpgsqlCommand(insertSql, conn))
                        {
                            // Adiciona parâmetros ao comando SQL
                            cmd.Parameters.AddWithValue("Nome", dados[1]);
                            cmd.Parameters.AddWithValue("Cidade", dados[2]);
                            cmd.Parameters.AddWithValue("Email", dados[3]);
                            cmd.Parameters.AddWithValue("CEP", dados[4]);
                            cmd.Parameters.AddWithValue("Endereco", dados[5]);
                            cmd.Parameters.AddWithValue("Pais", dados[6]);
                            cmd.Parameters.AddWithValue("Usuario", dados[7]);
                            cmd.Parameters.AddWithValue("Telefone", dados[8]);

                            // Tenta converter a data de nascimento para o formato adequado
                            DateTime dataNascimento;
                            string formatoData = "yyyy/MM/dd"; // Formato esperado da data
                            if (!DateTime.TryParseExact(dados[9], formatoData, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                            {
                                Console.WriteLine($"Data de nascimento inválida na linha: {linha}"); // Mensagem de erro se a data for inválida
                                continue; // Ignora a linha com erro
                            }

                            cmd.Parameters.AddWithValue("Data_Nascimento", dataNascimento);
                            cmd.Parameters.AddWithValue("Cargo_ID", int.Parse(dados[10])); // Adiciona o ID do cargo

                            await cmd.ExecuteNonQueryAsync(); // Executa o comando de inserção
                        }
                    }
                }
                Console.WriteLine("Dados importados para a tabela Pessoa com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar dados da tabela Pessoa: {ex.Message}"); // Mensagem de erro ao importar
            }

            // Importa dados da tabela Cargo
            try
            {
                using (var reader = new StreamReader(arquivoCargo))
                {
                    bool isFirstLine = true; // Variável para ignorar o cabeçalho
                    while (!reader.EndOfStream)
                    {
                        var linha = await reader.ReadLineAsync(); // Lê cada linha do arquivo CSV
                        if (isFirstLine)
                        {
                            isFirstLine = false; // Ignora a primeira linha (cabeçalho)
                            continue;
                        }

                        var dados = linha.Split(','); // Divide os dados da linha

                        // Comando SQL para inserir dados na tabela Cargo
                        var insertSql = "INSERT INTO Cargo(Nome, Salario) VALUES(@Nome, @Salario)";

                        using (var cmd = new NpgsqlCommand(insertSql, conn))
                        {
                            // Adiciona parâmetros ao comando SQL
                            cmd.Parameters.AddWithValue("Nome", dados[1]);
                            cmd.Parameters.AddWithValue("Salario", decimal.Parse(dados[2])); // Converte e adiciona o salário

                            await cmd.ExecuteNonQueryAsync(); // Executa o comando de inserção
                        }
                    }
                }
                Console.WriteLine("Dados importados para a tabela Cargo com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar dados da tabela Cargo: {ex.Message}"); // Mensagem de erro ao importar
            }

            // Preenche a tabela Pessoa_Salario com dados das tabelas Pessoa e Cargo
            var preencherPessoaSalarioSql = @"
                INSERT INTO pessoa_salario (nome, salario)
                SELECT p.nome, c.salario
                FROM pessoa p
                JOIN cargo c ON p.cargo_id = c.id;
            ";

            // Executa o comando para preencher a tabela Pessoa_Salario
            try
            {
                using (var cmd = new NpgsqlCommand(preencherPessoaSalarioSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync(); // Executa o comando de inserção
                    Console.WriteLine("Dados da tabela pessoa_salario preenchidos com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher tabela pessoa_salario: {ex.Message}"); // Mensagem de erro ao preencher
            }
        }
    }
}
