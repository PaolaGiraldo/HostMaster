using HostMaster.Backend.UnitsOfWork.Implementations;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HostMaster.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccommodationsController : GenericController<Accommodation>
{
    private readonly IAccomodationsUnitOfWork _accomodationsUnitOfWork;

    public AccommodationsController(IGenericUnitOfWork<Accommodation> unitOfWork, IAccomodationsUnitOfWork accomodationsUnitOfWork) : base(unitOfWork)
    {
        _accomodationsUnitOfWork = accomodationsUnitOfWork;
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _accomodationsUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _accomodationsUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _accomodationsUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("totalRecordsPaginated")]
    public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _accomodationsUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(AccommodationCreateDTO accommodationCreateDTO)
    {
        var action = await _accomodationsUnitOfWork.UpdateAsync(accommodationCreateDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
}