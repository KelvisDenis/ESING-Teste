using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Domain.Entities.Models
{
    public class People
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Pais { get; set; }
        public string DataNascimento { get; set; }
        public string IDCargo { get; set; }

        public People() { }

        public People(int iD, string nome, string cidade, string email, string telefone)
        {
            ID = iD;
            Nome = nome;
            Cidade = cidade;
            Email = email;
            Telefone = telefone ;
        }
    }

}