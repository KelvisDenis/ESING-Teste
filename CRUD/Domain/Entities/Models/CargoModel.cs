using System;  // Importa o namespace para usar tipos básicos
using System.Collections.Generic;  // Importa o namespace para usar coleções genéricas
using System.Linq;  // Importa o namespace para usar LINQ
using System.Web;  // Importa o namespace para funcionalidades do ASP.NET

namespace CRUD.Domain.Entities.Models
{
    /// <summary>
    /// Classe que representa um cargo dentro do sistema.
    /// </summary>
    public class CargoModel
    {
        // Propriedades do modelo de cargo
        public int ID { get; set; }  // Identificador único do cargo
        public string Name { get; set; }  // Nome do cargo
        public int Salary { get; set; }  // Salário associado ao cargo

        /// <summary>
        /// Construtor padrão da classe CargoModel.
        /// </summary>
        public CargoModel() { }

        /// <summary>
        /// Construtor da classe CargoModel que inicializa as propriedades.
        /// </summary>
        /// <param name="iD">ID do cargo.</param>
        /// <param name="name">Nome do cargo.</param>
        /// <param name="salary">Salário do cargo.</param>
        public CargoModel(int iD, string name, int salary)
        {
            ID = iD;  // Inicializa a propriedade ID
            Name = name;  // Inicializa a propriedade Name
            Salary = salary;  // Inicializa a propriedade Salary
        }
    }
}
