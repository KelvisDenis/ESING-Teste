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
    public class CargoService
    {
        private readonly string _connectionString;
        private readonly CargoRepository _cargoRepository;


        public CargoService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
            _cargoRepository = new CargoRepository(_connectionString);
        }
        public async Task<bool> AddCargoAsync(CargoModel model)
        {

            var response = await _cargoRepository.AddCargoAsync(model);
            return response;
        }
        public async Task<CargoModel> GetCargoByIDAsync(int id)
        {

            var response = await _cargoRepository.GetCargoByIDAsync(id);
            return response;
        }
        public async Task<CargoModel> GetCargoByNameAsync(string name)
        {

            var response = await _cargoRepository.GetCargoByNameAsync(name);
            return response;
        }
        public async Task<IEnumerable<CargoModel>> GetAllCargoAsync()
        {

            var response = await _cargoRepository.GetAllCargoAsync();
            return response;
        }
        public async Task<bool> UpdatePeopleAsync(CargoModel update)
        {

            var response = await _cargoRepository.UpdateCargoAsync(update);
            return response;
        }
        public async Task<bool> RemovePeopleAsync(int id)
        {

            var response = await _cargoRepository.RemoveCargoAsync(id);
            return response;
        }
    }
}