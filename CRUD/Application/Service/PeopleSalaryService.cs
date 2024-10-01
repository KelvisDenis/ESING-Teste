﻿using CRUD.Domain.Entities.Models;
using CRUD.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUD.Application.Service
{
    public class PeopleSalaryService
    {
        private readonly string _connectionString;
        private readonly PeopleSalaryRepository _peopleSalaryRepository;


        public PeopleSalaryService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            _peopleSalaryRepository = new PeopleSalaryRepository(_connectionString);
        }
        public async Task<bool> AddPeopleSalaryAsync(PeopleSalaryModel model)
        {

            var response = await _peopleSalaryRepository.AddPeopleSalaryAsync(model);
            return response;
        }
        public async Task<PeopleSalaryModel> GetPeopleSalaryByIDAsync(int id)
        {

            var response = await _peopleSalaryRepository.GetPeopleSalarByIDAsync(id);
            return response;
        }
        public async Task<PeopleSalaryModel> GetPeopleSalaryByNameAsync(string name)
        {

            var response = await _peopleSalaryRepository.GetPeopleSalarByNameAsync(name);
            return response;
        }
        public async Task<IEnumerable<PeopleSalaryModel>> GetAllPeopleSalaryAsync(int pageNumber, int pageSize)
        {

            var response = await _peopleSalaryRepository.GetAllPeopleSalarAsync(pageNumber, pageSize);
            return response;
        }
        public async Task<int> GetTotalPeopleSalaryCountAsync()
        {
            return await _peopleSalaryRepository.GetTotalCountPeopleSalaryAsync();
        }
        public async Task<bool> UpdatePeopleSalaryAsync(PeopleSalaryModel update)
        {

            var response = await _peopleSalaryRepository.UpdatePeopleSalaryAsync(update);
            return response;
        }
        public async Task<bool> RemovePeopleSalaryAsync(int id)
        {
            var servicePeople = new PeopleService();

            var peopleSalary = await _peopleSalaryRepository.GetPeopleSalarByIDAsync(id);
            var people = await servicePeople.GetPeopleByNameAsync(peopleSalary.Name);

            var response = await _peopleSalaryRepository.RemovePeopleSalaryAsync(id);
            var removePeople = await servicePeople.RemovePeopleAsync(people.ID);


            return response;
        }
    }
}