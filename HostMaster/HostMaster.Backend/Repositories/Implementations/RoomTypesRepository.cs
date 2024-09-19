using HostMaster.Backend.Data;
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

    async Task<ActionResponse<IEnumerable<RoomType>>> IRoomTypesRepository.GetByIdAsync(int Id)
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
}