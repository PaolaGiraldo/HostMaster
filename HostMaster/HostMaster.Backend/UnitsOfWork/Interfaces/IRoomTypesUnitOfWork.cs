using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Interfaces;

public interface IRoomTypesUnitOfWork
{
    Task<ActionResponse<IEnumerable<RoomType>>> GetByIdAsync(int Id);

    Task<ActionResponse<RoomType>> AddAsync(RoomTypeDTO roomTypeDTO);

    Task<ActionResponse<RoomType>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<RoomType>>> GetAsync();

    Task<IEnumerable<RoomType>> GetComboAsync();

    Task<ActionResponse<IEnumerable<RoomType>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<RoomType>> UpdateAsync(RoomTypeDTO roomTypeDTO);
}