using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.IO;

namespace HostMaster.Backend.Repositories.Implementations;

public class RoomPhotosRepository : GenericRepository<RoomPhoto>, IRoomPhotosRepository
{
    private readonly DataContext _context;

    public RoomPhotosRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    async Task<ActionResponse<RoomPhoto>> IRoomPhotosRepository.AddAsync(RoomPhotoCreateDTO roomPhotoCreateDTO)
    {
        var roomPhoto = new RoomPhoto
        {
            RoomId = roomPhotoCreateDTO.RoomId,
            RoomPhotoName = roomPhotoCreateDTO.RoomPhotoName,
        };

        _context.Add(roomPhoto);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<RoomPhoto>
            {
                WasSuccess = true,
                Result = roomPhoto
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<RoomPhoto>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<RoomPhoto>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    async Task<ActionResponse<IEnumerable<RoomPhoto>>> IRoomPhotosRepository.GetByRoomIdAsync(int roomId)
    {
        {
            var roomPhoto = await _context.RoomPhotos
            .Where(r => r.RoomId == roomId)
            .ToListAsync();

            return new ActionResponse<IEnumerable<RoomPhoto>>
            {
                WasSuccess = true,
                Result = roomPhoto
            };
        }
    }
}