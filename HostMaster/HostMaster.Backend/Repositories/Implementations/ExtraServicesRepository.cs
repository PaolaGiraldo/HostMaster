﻿using Azure;
using HostMaster.Backend.Data;
using HostMaster.Backend.Helpers;
using HostMaster.Backend.Repositories.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using HostMaster.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HostMaster.Backend.Repositories.Implementations
{
    public class ExtraServicesRepository : GenericRepository<ExtraService>, IExtraServicesRepository
    {
        private readonly DataContext _context;

        public ExtraServicesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        async Task<ActionResponse<ExtraService>> IExtraServicesRepository.AddAsync(ExtraServiceDTO extraServiceDTO)
        {
            var extraService = new ExtraService
            {
                ServiceName = extraServiceDTO.ServiceName,
                ServiceDescription = extraServiceDTO.ServiceDescription,
            };

            _context.Add(extraService);

            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = true,
                    Result = extraService
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = false,
                    Message = "ERR003"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        async Task<ActionResponse<ExtraService>> IExtraServicesRepository.GetAsync(int id)
        {
            var extraService = await _context.ExtraServices
             .FirstOrDefaultAsync(r => r.Id == id);

            if (extraService == null)
            {
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = false,
                    Message = "ERR001"
                };
            }

            return new ActionResponse<ExtraService>
            {
                WasSuccess = true,
                Result = extraService
            };
        }

        async Task<ActionResponse<IEnumerable<ExtraService>>> IExtraServicesRepository.GetAsync()
        {
            var extraServices = await _context.ExtraServices
           .Include(es => es.Availabilities)
           .ToListAsync();

            return new ActionResponse<IEnumerable<ExtraService>>
            {
                WasSuccess = true,
                Result = extraServices
            };
        }

        async Task<ActionResponse<IEnumerable<ExtraService>>> IExtraServicesRepository.GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.ExtraServices
           .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ServiceName.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<ExtraService>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.ServiceName)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        async Task<IEnumerable<ExtraService>> IExtraServicesRepository.GetComboAsync()
        {
            return await _context.ExtraServices
            .ToListAsync();
        }

        async Task<ActionResponse<int>> IExtraServicesRepository.GetTotalRecordsAsync(PaginationDTO pagination)
        {
            var queryable = _context.ExtraServices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ServiceName.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }

        async Task<ActionResponse<ExtraService>> IExtraServicesRepository.UpdateAsync(ExtraServiceDTO extraServiceDTO)
        {
            var extraServiceExists = await _context.ExtraServices.FindAsync(extraServiceDTO.Id);
            if (extraServiceExists == null)
            {
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = false,
                    Message = "ERR004"
                };
            }

            var extraService = new ExtraService
            {
                Id = extraServiceDTO.Id,
                ServiceName = extraServiceDTO.ServiceName,
                Price = extraServiceDTO.Price,
                ServiceDescription = extraServiceDTO.ServiceDescription
            };

            _context.Update(extraService);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = true,
                    Result = extraService
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = false,
                    Message = "ERR003"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<ExtraService>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        async Task<ActionResponse<ServiceAvailability>> IExtraServicesRepository.AddAvailabilityAsync(ServiceAvailabilityDTO availabilityDTO)
        {
            try
            {
                var extraService = await _context.Set<ExtraService>()
                                                 .FirstOrDefaultAsync(es => es.Id == availabilityDTO.ServiceId);

                if (extraService == null)
                {
                    return new ActionResponse<ServiceAvailability>
                    {
                        WasSuccess = false,
                        Message = "The specified ExtraService does not exist."
                    };
                }

                var serviceAvailability = new ServiceAvailability
                {
                    ServiceId = availabilityDTO.ServiceId,
                    StartDate = availabilityDTO.StartDate,
                    EndDate = availabilityDTO.EndDate,
                    IsAvailable = availabilityDTO.IsAvailable
                };

                // Optional: Validate StartDate and EndDate
                if (serviceAvailability.StartDate > serviceAvailability.EndDate)
                {
                    return new ActionResponse<ServiceAvailability>
                    {
                        WasSuccess = false,
                        Message = "StartDate cannot be later than EndDate."
                    };
                }

                _context.Add(serviceAvailability);

                await _context.SaveChangesAsync();

                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = true,
                    Result = serviceAvailability
                };
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = "Database update failed: ERR003"
                };
            }
            catch (Exception ex)
            {
                // Handle generic exceptions
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        async Task<ActionResponse<ServiceAvailability>> IExtraServicesRepository.UpdateAvailabilityAsync(ServiceAvailabilityDTO availabilityDTO)
        {
            var serviceAvailabilityExists = await _context.ServiceAvailabilities.FindAsync(availabilityDTO.ServiceId);
            if (serviceAvailabilityExists == null)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = "ERR004"
                };
            }

            var serviceAvailability = new ServiceAvailability
            {
                ServiceId = availabilityDTO.ServiceId,
                EndDate = availabilityDTO.EndDate,
                StartDate = availabilityDTO.StartDate,
                IsAvailable = availabilityDTO.IsAvailable
            };

            _context.Update(serviceAvailability);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = true,
                    Result = serviceAvailability
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = "ERR003"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        async Task<ActionResponse<IEnumerable<ServiceAvailability>>> IExtraServicesRepository.GetAvailabilityAsync(int serviceId)
        {
            var serviceAvailability = await _context.ServiceAvailabilities
                .Where(sa => sa.ServiceId == serviceId)
                .ToListAsync();

            if (serviceAvailability == null)
            {
                return new ActionResponse<IEnumerable<ServiceAvailability>>
                {
                    WasSuccess = false,
                    Message = "ERR001"
                };
            }

            return new ActionResponse<IEnumerable<ServiceAvailability>>
            {
                WasSuccess = true,
                Result = serviceAvailability
            };
        }

        public async Task<ActionResponse<IEnumerable<ServiceAvailability>>> GetAvailabilitiesAsync()
        {
            var serviceAvailabilities = await _context.ServiceAvailabilities
           .ToListAsync();

            return new ActionResponse<IEnumerable<ServiceAvailability>>
            {
                WasSuccess = true,
                Result = serviceAvailabilities
            };
        }

        async Task<ActionResponse<ServiceAvailability>> IExtraServicesRepository.DeleteAvailabilityAsync(int ServiceAvailabilityId)
        {
            var serviceAvailability = await _context.ServiceAvailabilities.FindAsync(ServiceAvailabilityId);
            if (serviceAvailability == null)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = "ERR004"
                };
            }

            _context.Remove(serviceAvailability);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = true,
                    Result = serviceAvailability
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = "ERR003"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        async Task<ActionResponse<ServiceAvailability>> IExtraServicesRepository.GetAvailabilityByIdAsync(int Id)
        {
            var serviceAvailability = await _context.ServiceAvailabilities.FindAsync(Id);
            if (serviceAvailability == null)
            {
                return new ActionResponse<ServiceAvailability>
                {
                    WasSuccess = false,
                    Message = "ERR001"
                };
            }

            return new ActionResponse<ServiceAvailability>
            {
                WasSuccess = true,
                Result = serviceAvailability
            };
        }
    }
}