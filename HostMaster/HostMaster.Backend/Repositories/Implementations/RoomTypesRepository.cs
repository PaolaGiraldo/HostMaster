using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace HostMaster.Backend.Repositories.Implementations;

public class RoomTypesRepository : GenericRepository<RoomType>, IRoomTypesRepository
{
    private readonly DataContext _context;

    public RoomTypesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    async Task<ActionResponse<RoomType>> IRoomTypesRepository.AddAsync(RoomTypeDTO roomTypeDTO)
    {
        var roomType = new RoomType
        {
            TypeName = roomTypeDTO.TypeName,
            Description = roomTypeDTO.Description,
        };

        _context.Add(roomType);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<RoomType>
            {
                WasSuccess = true,
                Result = roomType
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<RoomType>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<RoomType>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    async Task<ActionResponse<IEnumerable<RoomType>>> IRoomTypesRepository.GetByRoomIdAsync(int Id)
    {
        {
            var roomType = await _context.RoomTypes
            .Where(r => r.Id == Id)
            .ToListAsync();

            return new ActionResponse<IEnumerable<RoomType>>
            {
                WasSuccess = true,
                Result = roomType
            };
        }
    }

    async Task<ActionResponse<RoomType>> IRoomTypesRepository.GetAsync(int id)
    {
        var roomType = await _context.RoomTypes
             .FirstOrDefaultAsync(r => r.Id == id);

        if (roomType == null)
        {
            return new ActionResponse<RoomType>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<RoomType>
        {
            WasSuccess = true,
            Result = roomType
        };
    }

    async Task<ActionResponse<IEnumerable<RoomType>>> IRoomTypesRepository.GetAsync()
    {
        var roomTypes = await _context.RoomTypes
           .ToListAsync();

        return new ActionResponse<IEnumerable<RoomType>>
        {
            WasSuccess = true,
            Result = roomTypes
        };
    }

    async Task<ActionResponse<IEnumerable<RoomType>>> IRoomTypesRepository.GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.RoomTypes
           .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.TypeName.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<RoomType>>
        {
            WasSuccess = true,
            Result = await queryable
                .OrderBy(x => x.TypeName)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    async Task<IEnumerable<RoomType>> IRoomTypesRepository.GetComboAsync()
    {
        return await _context.RoomTypes
            .ToListAsync();
    }

    async Task<ActionResponse<int>> IRoomTypesRepository.GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.RoomTypes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.TypeName.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    async Task<ActionResponse<RoomType>> IRoomTypesRepository.UpdateAsync(RoomTypeDTO roomTypeDTO)
    {
        var cityalreadyexists = await _context.RoomTypes.FindAsync(roomTypeDTO.Id);
        if (cityalreadyexists == null)
        {
            return new ActionResponse<RoomType>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var roomType = new RoomType
        {
            Id = roomTypeDTO.Id,
            TypeName = roomTypeDTO.TypeName,
            Price = roomTypeDTO.Price,
            MaxGuests = roomTypeDTO.MaxGuests
        };

        _context.Update(roomType);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<RoomType>
            {
                WasSuccess = true,
                Result = roomType
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<RoomType>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<RoomType>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }
}