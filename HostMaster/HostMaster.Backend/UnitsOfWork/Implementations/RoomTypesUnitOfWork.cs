using HostMaster.Backend.Repositories.Implementations;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Implementations;

public class RoomTypesUnitOfWork : GenericUnitOfWork<RoomType>, IRoomTypesUnitOfWork
{
    private readonly IRoomTypesRepository _roomTypesRepository;

    public RoomTypesUnitOfWork(IGenericRepository<RoomType> repository, IRoomTypesRepository roomTypesRepository) : base(repository)
    {
        _roomTypesRepository = roomTypesRepository;
    }

    public async Task<ActionResponse<RoomType>> AddAsync(RoomTypeDTO roomTypeDTO) => await _roomTypesRepository.AddAsync(roomTypeDTO);

    public async Task<ActionResponse<IEnumerable<RoomType>>> GetByIdAsync(int Id) => await _roomTypesRepository.GetByRoomIdAsync(Id);

    public override async Task<ActionResponse<IEnumerable<RoomType>>> GetAsync() => await _roomTypesRepository.GetAsync();

    public override async Task<ActionResponse<RoomType>> GetAsync(int id) => await _roomTypesRepository.GetAsync(id);

    public override async Task<ActionResponse<IEnumerable<RoomType>>> GetAsync(PaginationDTO pagination) => await _roomTypesRepository.GetAsync(pagination);

    public async Task<IEnumerable<RoomType>> GetComboAsync() => await _roomTypesRepository.GetComboAsync();

    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _roomTypesRepository.GetTotalRecordsAsync(pagination);

    public async Task<ActionResponse<RoomType>> UpdateAsync(RoomTypeDTO roomTypeDTO) => await _roomTypesRepository.UpdateAsync(roomTypeDTO);
}