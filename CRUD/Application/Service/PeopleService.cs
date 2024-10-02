using CRUD.Domain.Entities.Models;
using CRUD.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUD.Application.Service
{
    public class PeopleService
    {
        private readonly string _connectionString;
        private readonly PeopleRepository _peopleRepository;


        public PeopleService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            _peopleRepository = new PeopleRepository(_connectionString);
        }
        public async Task<bool> AddPeopleAsync(People model)
        {
                var servicePeopleSalary = new PeopleSalaryService();
                var serviceCargo = new CargoService();

                var cargoModel = await serviceCargo.GetCargoByIDAsync(model.IDCargo);
                var peopleSalaryModel = new PeopleSalaryModel(model.ID, model.Nome, cargoModel.Salary);

                var response = await _peopleRepository.AddPeopleAsync(model);
                var resposePeopleSalary = await servicePeopleSalary.AddPeopleSalaryAsync(peopleSalaryModel);

                return response;
        }
        public async Task<People> GetPeopleByIDAsync(int id)
        {

            var response = await _peopleRepository.GetPeopleByIDAsync(id);
            return response;
        }
        public async Task<People> GetPeopleByNameAsync(string name)
        {

            var response = await _peopleRepository.GetPeopleByNameAsync(name);
            return response;
        }
        public async Task<IEnumerable<People>> GetAllPeopleAsync()
        {

            var response = await _peopleRepository.GetAllPeopleAsync();
            return response;
        }
        public async Task<bool> UpdatePeopleAsync(People update)
        {
            var servicePeopleSalary = new PeopleSalaryService();
            var serviceCargo = new CargoService();

            var cargoModel = await serviceCargo.GetCargoByIDAsync(update.IDCargo);
            var peopleSalaryModel = await servicePeopleSalary.GetPeopleSalaryByNameAsync(update.Nome);

            peopleSalaryModel.Name = update.Nome;
            peopleSalaryModel.Salary = cargoModel.Salary;

            var response = await _peopleRepository.UpdatePeopleAsync(update);
            var responsePeopleSalary = await servicePeopleSalary.UpdatePeopleSalaryAsync(peopleSalaryModel);

            return response;
        }
        public async Task<bool> RemovePeopleAsync(int id)
        {
            var response = await _peopleRepository.RemovePeopleAsync(id);

            return response;
        }
    }
}