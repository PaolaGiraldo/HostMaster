using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Interfaces;

public interface IRoomTypesUnitOfWork
{
    Task<ActionResponse<IEnumerable<RoomType>>> GetByIdAsync(int Id);

    Task<ActionResponse<RoomType>> AddAsync(RoomTypeDTO roomTypeDTO);
}