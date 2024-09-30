﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Domain.Entities.Models
{
    public class PeopleSalaryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }


        public PeopleSalaryModel() { }
        public PeopleSalaryModel(int iD, string name, int salary)
        {
            ID = iD;
            Name = name;
            Salary = salary;
        }
    }
}