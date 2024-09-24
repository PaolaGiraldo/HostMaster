using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.IO;

namespace HostMaster.Backend.Repositories.Implementations;

public class RoomsRepository : GenericRepository<Room>, IRoomsRepository
{
    private readonly DataContext _context;
    private readonly IFileStorage _fileStorage;

    public RoomsRepository(DataContext context, IFileStorage fileStorage) : base(context)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public override async Task<ActionResponse<Room>> GetAsync(int id)
    {
        var room = await _context.Rooms
             .Include(r => r.Accommodation)
             .ThenInclude(r => r.City)
             .Include(r => r.RoomType)
             .Include(r => r.Photos)
             .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Room>
        {
            WasSuccess = true,
            Result = room
        };
    }

    public override async Task<ActionResponse<IEnumerable<Room>>> GetAsync()
    {
        var rooms = await _context.Rooms
            .Include(r => r.Accommodation)
            .Include(r => r.RoomType)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Room>>
        {
            WasSuccess = true,
            Result = rooms
        };
    }

    public async Task<IEnumerable<Room>> GetComboAsync()
    {
        return await _context.Rooms
            .OrderBy(x => x.RoomNumber)
            .ToListAsync();
    }

    public override async Task<ActionResponse<IEnumerable<Room>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Rooms
            .Include(x => x.RoomType)
            .Include(x => x.Photos)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.RoomNumber.ToLower().Contains(pagination.Filter.ToLower())
            && x.AccommodationId == pagination.Id);
        }

        return new ActionResponse<IEnumerable<Room>>
        {
            WasSuccess = true,
            Result = await queryable
                .Where(x => x.AccommodationId == pagination.Id)
                .OrderBy(x => x.RoomNumber)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Rooms.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.RoomNumber.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
    {
        return await _context.Rooms
            .Include(r => r.Accommodation)
            .Include(r => r.RoomType)
            .Where(r => r.IsAvailable)
            .ToListAsync();
    }

    public async Task<ActionResponse<Room>> AddAsync(RoomCreateDTO roomCreateDTO)
    {
        var accomodation = await _context.Accommodations.FindAsync(roomCreateDTO.AccommodationId);
        if (accomodation == null)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var roomType = await _context.RoomTypes.FindAsync(roomCreateDTO.RoomTypeId);
        if (roomType == null)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var room = new Room
        {
            RoomNumber = roomCreateDTO.RoomNumber,
            //Price = roomCreateDTO.Price,
            IsAvailable = roomCreateDTO.IsAvailable,
            Accommodation = accomodation,
            RoomType = roomType,
        };

        /*  if (roomCreateDTO.Photos != null && roomCreateDTO.Photos.Any())
          {
              foreach (var photo in roomCreateDTO.Photos)
              {
                  // Procesar y cambiar el nombre de la foto (RoomPhotoName)
                  string originalName = photo.RoomPhotoName;

                  var imageBase64 = Convert.FromBase64String(originalName);

                  // Cambiar la imagen en base64 completamente por el sha
                  photo.RoomPhotoName = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "rooms");

                  Console.WriteLine($"Nuevo nombre de la foto procesada: {photo.RoomPhotoName}");
              }

              room.Photos = roomCreateDTO.Photos;
          }
          else
          {
              return new ActionResponse<Room>
              {
                  WasSuccess = false,
                  Message = "ERR004"
              };
          }*/

        _context.Add(room);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Room>
            {
                WasSuccess = true,
                Result = room
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    async Task<ActionResponse<IEnumerable<Room>>> IRoomsRepository.GetByAccommodationIdAsync(int accommodationId)
    {
        var room = await _context.Rooms
        .Where(r => r.AccommodationId == accommodationId)
        .Include(r => r.Accommodation)
        .ThenInclude(a => a.City)
        .Include(r => r.RoomType)
        .Include(r => r.Reservations)
        .Include(r => r.RoomInventoryItems)
        .Include(r => r.Photos)
        .ToListAsync();

        return new ActionResponse<IEnumerable<Room>>
        {
            WasSuccess = true,
            Result = room
        };
    }

    public async Task<ActionResponse<Room>> UpdateAsync(RoomCreateDTO roomCreateDTO)
    {
        var accomodation = await _context.Accommodations.FindAsync(roomCreateDTO.AccommodationId);
        if (accomodation == null)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR004X"
            };
        }

        Console.WriteLine($"room type obtneida: {roomCreateDTO.RoomTypeId}");

        var roomType = await _context.RoomTypes.FindAsync(roomCreateDTO.RoomTypeId);
        if (roomType == null)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var room = new Room
        {
            RoomNumber = roomCreateDTO.RoomNumber,
            //Price = roomCreateDTO.Price,
            IsAvailable = roomCreateDTO.IsAvailable,
            Accommodation = accomodation,
            RoomType = roomType,
        };

        /*if (roomCreateDTO.Photos != null && roomCreateDTO.Photos.Any())
        {
            foreach (var photo in roomCreateDTO.Photos)
            {
                Console.WriteLine("PASO 1");
                // Procesar y cambiar el nombre de la foto (RoomPhotoName)
                string originalName = photo.RoomPhotoName;
                Console.WriteLine("PASO 2");
                var imageBase64 = Convert.FromBase64String(originalName);
                Console.WriteLine("PASO 3");
                // Cambiar la imagen en base64 completamente por el sha
                photo.RoomPhotoName = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
                Console.WriteLine("PASO 4");
                Console.WriteLine($"Nuevo nombre de la foto procesada: {photo.RoomPhotoName}");
                Console.WriteLine("PASO 5");
                _context.Add(photo);
            }

            // room.Photos = roomCreateDTO.Photos;
        }*/

        _context.Update(room);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Room>
            {
                WasSuccess = true,
                Result = room
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Room>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }
}