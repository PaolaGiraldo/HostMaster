using HostMaster.Backend.Repositories.Implementations;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.UnitsOfWork.Implementations
{
    public class RoomPhotosUnitOfWork : GenericUnitOfWork<RoomPhoto>, IRoomPhotosUnitOfWork
    {
        private readonly IRoomPhotosRepository _roomsPhotosRepository;

        public RoomPhotosUnitOfWork(IGenericRepository<RoomPhoto> repository, IRoomPhotosRepository roomsPhotosRepository) : base(repository)
        {
            _roomsPhotosRepository = roomsPhotosRepository;
        }

        public async Task<ActionResponse<RoomPhoto>> AddAsync(RoomPhotoCreateDTO roomPhotoCreateDTO) => await _roomsPhotosRepository.AddAsync(roomPhotoCreateDTO);

        public async Task<ActionResponse<IEnumerable<RoomPhoto>>> GetByRoomIdAsync(int roomId) => await _roomsPhotosRepository.GetByRoomIdAsync(roomId);

        public async Task<ActionResponse<RoomPhoto>> UpdateAsync(RoomPhotoCreateDTO roomPhotoCreateDTO) => await _roomsPhotosRepository.AddAsync(roomPhotoCreateDTO);

        public async Task<ActionResponse<RoomPhoto>> DeleteByRoomIdAsync(int roomId) => await _roomsPhotosRepository.DeleteByRoomIdAsync(roomId);
    }
}