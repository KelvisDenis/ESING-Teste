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
            
                var response = await _peopleRepository.AddPeopleAsync(model);
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

            var response = await _peopleRepository.UpdatePeopleAsync(update);
            return response;
        }
        public async Task<bool> RemovePeopleAsync(int id)
        {

            var response = await _peopleRepository.RemovePeopleAsync(id);
            return response;
        }
    }
}