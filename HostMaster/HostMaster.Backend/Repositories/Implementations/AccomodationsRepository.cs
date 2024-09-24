using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace HostMaster.Backend.Repositories.Implementations;

public class AccomodationsRepository : GenericRepository<RoomType>, IAccomodationsRepository
{
    private readonly DataContext _context;

    public AccomodationsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    async Task<ActionResponse<Accommodation>> IAccomodationsRepository.AddAsync(AccommodationCreateDTO accommodationCreateDTO)
    {
        var accomodation = await _context.Accommodations.FindAsync(accommodationCreateDTO.Id);
        if (accomodation != null)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var accommodation = new Accommodation
        {
            Id = accommodationCreateDTO.Id,
            Name = accommodationCreateDTO.Name,
            Address = accommodationCreateDTO.Address,
            PhoneNumber = accommodationCreateDTO.PhoneNumber,
            CityId = accommodationCreateDTO.CityId,
        };

        _context.Add(accommodation);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Accommodation>
            {
                WasSuccess = true,
                Result = accommodation
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    async Task<ActionResponse<Accommodation>> IAccomodationsRepository.GetAsync(int id)
    {
        var accommodation = await _context.Accommodations
            .Include(r => r.City)
             .FirstOrDefaultAsync(r => r.Id == id);

        if (accommodation == null)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Accommodation>
        {
            WasSuccess = true,
            Result = accommodation
        };
    }

    async Task<ActionResponse<IEnumerable<Accommodation>>> IAccomodationsRepository.GetAsync()
    {
        var rooms = await _context.Accommodations
           .Include(r => r.City)
           .ToListAsync();

        return new ActionResponse<IEnumerable<Accommodation>>
        {
            WasSuccess = true,
            Result = rooms
        };
    }

    async Task<ActionResponse<IEnumerable<Accommodation>>> IAccomodationsRepository.GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Accommodations
           .Include(x => x.City)
           .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower())
            && x.Id == pagination.Id);
        }

        return new ActionResponse<IEnumerable<Accommodation>>
        {
            WasSuccess = true,
            Result = await queryable
                .Where(x => x.Id == pagination.Id)
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    async Task<IEnumerable<Accommodation>> IAccomodationsRepository.GetComboAsync()
    {
        return await _context.Accommodations
            .Include(r => r.City)
            .ToListAsync();
    }

    async Task<ActionResponse<int>> IAccomodationsRepository.GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Accommodations.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    async Task<ActionResponse<Accommodation>> IAccomodationsRepository.UpdateAsync(AccommodationCreateDTO accommodationCreateDTO)
    {
        var accomodation = await _context.Accommodations.FindAsync(accommodationCreateDTO.Id);
        if (accomodation == null)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var accommodation = new Accommodation
        {
            Id = accommodationCreateDTO.Id,
            Name = accommodationCreateDTO.Name,
            Address = accommodationCreateDTO.Address,
            PhoneNumber = accommodationCreateDTO.PhoneNumber,
            CityId = accommodationCreateDTO.CityId,
        };

        _context.Update(accommodation);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Accommodation>
            {
                WasSuccess = true,
                Result = accommodation
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Accommodation>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }
}