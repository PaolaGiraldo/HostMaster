using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Implementations;

public class RoomsUnitOfWork : GenericUnitOfWork<Room>, IRoomsUnitOfWork

{
    private readonly IRoomsRepository _roomsRepository;

    public RoomsUnitOfWork(IGenericRepository<Room> repository, IRoomsRepository roomsRepository) : base(repository)
    {
        _roomsRepository = roomsRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Room>>> GetAsync() => await _roomsRepository.GetAsync();

    public async Task<ActionResponse<IEnumerable<Room>>> GetByAccommodationIdAsync(int accommodationId) => await _roomsRepository.GetByAccommodationIdAsync(accommodationId);

    public override async Task<ActionResponse<Room>> GetAsync(int id) => await _roomsRepository.GetAsync(id);

    public override async Task<ActionResponse<IEnumerable<Room>>> GetAsync(PaginationDTO pagination) => await _roomsRepository.GetAsync(pagination);

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync() => await _roomsRepository.GetAvailableRoomsAsync();

    public async Task<IEnumerable<Room>> GetComboAsync() => await _roomsRepository.GetComboAsync();

    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _roomsRepository.GetTotalRecordsAsync(pagination);

    public async Task<ActionResponse<Room>> UpdateAsync(RoomCreateDTO roomCreateDTO) => await _roomsRepository.UpdateAsync(roomCreateDTO);
}