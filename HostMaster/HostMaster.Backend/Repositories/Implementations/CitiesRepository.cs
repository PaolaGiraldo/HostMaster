using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace HostMaster.Backend.Repositories.Implementations
{
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        private readonly DataContext _context;

        public CitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        async Task<ActionResponse<City>> ICitiesRepository.AddAsync(CityCreateDTO cityCreateDTO)
        {
            var states = await _context.States.FindAsync(cityCreateDTO.Id);
            if (states != null)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = "ERR004"
                };
            }

            var city = new City
            {
                Name = cityCreateDTO.Name,
                StateId = cityCreateDTO.StateId
            };

            _context.Add(city);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<City>
                {
                    WasSuccess = true,
                    Result = city
                };
            }
            catch (DbUpdateException exception)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        async Task<ActionResponse<City>> ICitiesRepository.GetAsync(int id)
        {
            var city = await _context.Cities
                .Include(r => r.State)
                 .FirstOrDefaultAsync(r => r.Id == id);

            if (city == null)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = "ERR001"
                };
            }

            return new ActionResponse<City>
            {
                WasSuccess = true,
                Result = city
            };
        }

        async Task<ActionResponse<IEnumerable<City>>> ICitiesRepository.GetAsync()
        {
            var cities = await _context.Cities
               .Include(r => r.State)
               .ToListAsync();

            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = cities
            };
        }

        async Task<ActionResponse<IEnumerable<City>>> ICitiesRepository.GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities
               .Include(x => x.State)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower())
                && x.StateId == pagination.Id);
            }

            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = await queryable
                    .Where(x => x.StateId == pagination.Id)
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        async Task<IEnumerable<City>> ICitiesRepository.GetComboAsync()
        {
            return await _context.Cities
                .Include(r => r.State)
                .ToListAsync();
        }

        async Task<ActionResponse<int>> ICitiesRepository.GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities.AsQueryable();

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

        async Task<ActionResponse<City>> ICitiesRepository.UpdateAsync(CityCreateDTO cityCreateDTO)
        {
            var cityalreadyexists = await _context.Cities.FindAsync(cityCreateDTO.Id);
            if (cityalreadyexists == null)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = "ERR004"
                };
            }

            var city = new City
            {
                Id = cityCreateDTO.Id,
                Name = cityCreateDTO.Name,
                StateId = cityCreateDTO.StateId
            };

            _context.Update(city);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<City>
                {
                    WasSuccess = true,
                    Result = city
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = "ERR003"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }
    }
}