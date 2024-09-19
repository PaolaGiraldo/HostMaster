using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.Repositories.Interfaces;

public interface IRoomTypesRepository
{
    Task<ActionResponse<IEnumerable<RoomType>>> GetByIdAsync(int roomId);

    Task<ActionResponse<RoomType>> AddAsync(RoomTypeDTO roomTypeDTO);
}