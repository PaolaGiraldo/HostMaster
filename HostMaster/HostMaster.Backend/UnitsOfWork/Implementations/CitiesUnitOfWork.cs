using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.Entities;
using HostMaster.Shared.DTOs;

using HostMaster.Shared.Responses;
using HostMaster.Backend.Repositories.Implementations;

namespace HostMaster.Backend.UnitsOfWork.Implementations
{
    public class CitiesUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesUnitOfWork(IGenericRepository<City> repository, ICitiesRepository citiesRepository) : base(repository)
        {
            _citiesRepository = citiesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync() => await _citiesRepository.GetAsync();

        public override async Task<ActionResponse<City>> GetAsync(int id) => await _citiesRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination) => await _citiesRepository.GetAsync(pagination);

        public async Task<IEnumerable<City>> GetComboAsync() => await _citiesRepository.GetComboAsync();

        public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _citiesRepository.GetTotalRecordsAsync(pagination);

        public async Task<ActionResponse<City>> UpdateAsync(CityCreateDTO cityCreateDTO) => await _citiesRepository.UpdateAsync(cityCreateDTO);
    }
}