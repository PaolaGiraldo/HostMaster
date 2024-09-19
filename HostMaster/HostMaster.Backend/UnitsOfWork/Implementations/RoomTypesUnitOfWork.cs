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

    public async Task<ActionResponse<IEnumerable<RoomType>>> GetByIdAsync(int Id) => await _roomTypesRepository.GetByIdAsync(Id);

}