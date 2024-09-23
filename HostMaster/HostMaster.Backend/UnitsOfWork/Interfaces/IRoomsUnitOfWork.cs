using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Interfaces;

public interface IRoomsUnitOfWork
{
    Task<ActionResponse<Room>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Room>>> GetAsync();

    Task<ActionResponse<IEnumerable<Room>>> GetByAccommodationIdAsync(int accommodationId);

    Task<IEnumerable<Room>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Room>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<IEnumerable<Room>> GetAvailableRoomsAsync();

    Task<ActionResponse<Room>> UpdateAsync(RoomCreateDTO roomCreateDTO);
}