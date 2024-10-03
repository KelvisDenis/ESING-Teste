using System;  // Importa o namespace para usar tipos básicos
using System.Collections.Generic;  // Importa o namespace para usar coleções genéricas
using System.Linq;  // Importa o namespace para usar LINQ
using System.Web;  // Importa o namespace para funcionalidades do ASP.NET

namespace CRUD.Domain.Entities.Models
{
    /// <summary>
    /// Classe que representa uma pessoa dentro do sistema.
    /// </summary>
    public class People
    {
        // Propriedades do modelo de pessoa
        public int ID { get; set; }  // Identificador único da pessoa
        public string Nome { get; set; }  // Nome da pessoa
        public string Cidade { get; set; }  // Cidade onde a pessoa reside
        public string Email { get; set; }  // Email da pessoa
        public string Telefone { get; set; }  // Número de telefone da pessoa
        public string CEP { get; set; }  // Código de Endereçamento Postal da pessoa
        public string Endereco { get; set; }  // Endereço completo da pessoa
        public string Usuario { get; set; }  // Nome de usuário da pessoa
        public string Pais { get; set; }  // País onde a pessoa reside
        public DateTime DataNascimento { get; set; }  // Data de nascimento da pessoa
        public int IDCargo { get; set; }  // ID do cargo associado à pessoa

        /// <summary>
        /// Construtor padrão da classe People.
        /// </summary>
        public People() { }

        /// <summary>
        /// Construtor da classe People que inicializa as propriedades.
        /// </summary>
        /// <param name="iD">ID da pessoa.</param>
        /// <param name="nome">Nome da pessoa.</param>
        /// <param name="cidade">Cidade onde a pessoa reside.</param>
        /// <param name="email">Email da pessoa.</param>
        /// <param name="telefone">Número de telefone da pessoa.</param>
        /// <param name="cEP">Código de Endereçamento Postal.</param>
        /// <param name="endereco">Endereço completo da pessoa.</param>
        /// <param name="usuario">Nome de usuário da pessoa.</param>
        /// <param name="pais">País da pessoa.</param>
        /// <param name="dataNascimento">Data de nascimento da pessoa.</param>
        /// <param name="iDCargo">ID do cargo associado à pessoa.</param>
        public People(int iD, string nome, string cidade, string email, string telefone, string cEP, string endereco,
            string usuario, string pais, DateTime dataNascimento, int iDCargo)
        {
            ID = iD;  // Inicializa a propriedade ID
            Nome = nome;  // Inicializa a propriedade Nome
            Cidade = cidade;  // Inicializa a propriedade Cidade
            Email = email;  // Inicializa a propriedade Email
            Telefone = telefone;  // Inicializa a propriedade Telefone
            CEP = cEP;  // Inicializa a propriedade CEP
            Endereco = endereco;  // Inicializa a propriedade Endereco
            Usuario = usuario;  // Inicializa a propriedade Usuario
            Pais = pais;  // Inicializa a propriedade Pais
            DataNascimento = dataNascimento;  // Inicializa a propriedade DataNascimento
            IDCargo = iDCargo;  // Inicializa a propriedade IDCargo
        }
    }
}
