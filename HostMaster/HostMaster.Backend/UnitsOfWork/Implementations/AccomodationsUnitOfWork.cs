using HostMaster.Backend.Repositories.Implementations;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Implementations;

public class AccomodationsUnitOfWork : GenericUnitOfWork<Accommodation>, IAccomodationsUnitOfWork
{
    private readonly IAccomodationsRepository _accomodationsRepository;

    public AccomodationsUnitOfWork(IGenericRepository<Accommodation> repository, IAccomodationsRepository accomodationsRepository) : base(repository)
    {
        _accomodationsRepository = accomodationsRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Accommodation>>> GetAsync() => await _accomodationsRepository.GetAsync();

    public override async Task<ActionResponse<Accommodation>> GetAsync(int id) => await _accomodationsRepository.GetAsync(id);

    public override async Task<ActionResponse<IEnumerable<Accommodation>>> GetAsync(PaginationDTO pagination) => await _accomodationsRepository.GetAsync(pagination);

    public async Task<IEnumerable<Accommodation>> GetComboAsync() => await _accomodationsRepository.GetComboAsync();

    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _accomodationsRepository.GetTotalRecordsAsync(pagination);

    public async Task<ActionResponse<Accommodation>> UpdateAsync(AccommodationCreateDTO accommodationCreateDTO) => await _accomodationsRepository.UpdateAsync(accommodationCreateDTO);
}