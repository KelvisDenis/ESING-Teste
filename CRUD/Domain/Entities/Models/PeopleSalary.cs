using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Domain.Entities.Models
{
    public class PeopleSalary
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }


        public PeopleSalary() { }
        public PeopleSalary(int iD, string name, int salary)
        {
            ID = iD;
            Name = name;
            Salary = salary;
        }
    }
}