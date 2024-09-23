using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;

namespace HostMaster.Backend.Repositories.Interfaces;

public interface IRoomPhotosRepository
{
    Task<ActionResponse<IEnumerable<RoomPhoto>>> GetByRoomIdAsync(int roomId);

    Task<ActionResponse<RoomPhoto>> DeleteByRoomIdAsync(int roomId);

    Task<ActionResponse<RoomPhoto>> AddAsync(RoomPhotoCreateDTO roomPhotoCreateDTO);

    Task<ActionResponse<RoomPhoto>> UpdateAsync(RoomPhotoCreateDTO roomPhotoCreateDTO);
}