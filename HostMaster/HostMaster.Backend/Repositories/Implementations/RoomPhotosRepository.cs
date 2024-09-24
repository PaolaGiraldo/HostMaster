using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Diagnostics;

namespace HostMaster.Backend.Repositories.Implementations;

public class RoomPhotosRepository : GenericRepository<RoomPhoto>, IRoomPhotosRepository
{
    private readonly DataContext _context;

    private readonly IFileStorage _fileStorage;

    public RoomPhotosRepository(DataContext context, IFileStorage fileStorage) : base(context)
    {
        _context = context;

        _fileStorage = fileStorage;
    }

    async Task<ActionResponse<RoomPhoto>> IRoomPhotosRepository.AddAsync(RoomPhotoCreateDTO roomPhotoCreateDTO)
    {
        //var imageBase64 = Convert.FromBase64String(roomPhotoCreateDTO.RoomPhotoName!);
        //roomPhotoCreateDTO.RoomPhotoName = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "hostMaster");

        var roomPhoto = new RoomPhoto
        {
            RoomId = roomPhotoCreateDTO.RoomId,
            RoomPhotoURL = roomPhotoCreateDTO.RoomPhotoURL,
        };

        var imageBase64 = Convert.FromBase64String(roomPhotoCreateDTO.RoomPhotoURL!);
        roomPhoto.RoomPhotoURL = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");

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

    async Task<ActionResponse<RoomPhoto>> IRoomPhotosRepository.UpdateAsync(RoomPhotoCreateDTO roomPhotoCreateDTO)
    {
        var imageBase64 = Convert.FromBase64String(roomPhotoCreateDTO.RoomPhotoURL!);
        roomPhotoCreateDTO.RoomPhotoURL = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "hostMaster");

        Debug.WriteLine("BASE 64 FILE");
        Debug.WriteLine(roomPhotoCreateDTO.RoomPhotoURL);

        var roomPhoto = new RoomPhoto
        {
            RoomId = roomPhotoCreateDTO.RoomId,
            RoomPhotoURL = roomPhotoCreateDTO.RoomPhotoURL,
        };

        _context.Update(roomPhoto);

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

    async Task<ActionResponse<RoomPhoto>> IRoomPhotosRepository.DeleteByRoomIdAsync(int roomId)
    {
        var photosToDelete = await _context.RoomPhotos
                                   .Where(photo => photo.RoomId == roomId)
                                   .ToListAsync();

        if (photosToDelete == null || !photosToDelete.Any())
        {
            return new ActionResponse<RoomPhoto>
            {
                WasSuccess = true,
                Message = "Photos not found"
            };
        }

        _context.RoomPhotos.RemoveRange(photosToDelete);
        await _context.SaveChangesAsync();

        foreach (var photo in photosToDelete)
        {
            await _fileStorage.RemoveFileAsync(photo.RoomPhotoURL, "teams");
        }

        return new ActionResponse<RoomPhoto>
        {
            WasSuccess = true,
        };
    }
}