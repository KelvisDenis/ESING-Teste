using System;  // Importa o namespace para usar tipos básicos
using System.Collections.Generic;  // Importa o namespace para usar coleções genéricas
using System.Linq;  // Importa o namespace para usar LINQ
using System.Web;  // Importa o namespace para funcionalidades do ASP.NET

namespace CRUD.Domain.Entities.Models
{
    /// <summary>
    /// Classe que representa o modelo de salário de uma pessoa.
    /// </summary>
    public class PeopleSalaryModel
    {
        public int? ID { get; set; }  // Identificador único do salário da pessoa (pode ser nulo)
        public string Name { get; set; }  // Nome da pessoa
        public int? Salary { get; set; }  // Salário da pessoa (pode ser nulo)

        /// <summary>
        /// Construtor padrão da classe PeopleSalaryModel.
        /// </summary>
        public PeopleSalaryModel() { }

        /// <summary>
        /// Construtor da classe PeopleSalaryModel que inicializa as propriedades.
        /// </summary>
        /// <param name="iD">ID do salário da pessoa.</param>
        /// <param name="name">Nome da pessoa.</param>
        /// <param name="salary">Salário da pessoa.</param>
        public PeopleSalaryModel(int? iD, string name, int? salary)
        {
            ID = iD;  // Inicializa a propriedade ID
            Name = name;  // Inicializa a propriedade Name
            Salary = salary;  // Inicializa a propriedade Salary
        }
    }
}
