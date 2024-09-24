using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.Repositories.Interfaces;

public interface IAccomodationsRepository
{
    Task<ActionResponse<Accommodation>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Accommodation>>> GetAsync();

    Task<IEnumerable<Accommodation>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Accommodation>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<Accommodation>> AddAsync(AccommodationCreateDTO accommodationCreateDTO);

    Task<ActionResponse<Accommodation>> UpdateAsync(AccommodationCreateDTO accommodationCreateDTO);
}