using HostMaster.Backend.Controllers;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HostMaster.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : GenericController<Room>
{
    private readonly IRoomsUnitOfWork _roomsUnitOfWork;

    public RoomsController(IGenericUnitOfWork<Room> unitOfWork, IRoomsUnitOfWork roomsUnitOfWork) : base(unitOfWork)
    {
        _roomsUnitOfWork = roomsUnitOfWork;
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _roomsUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _roomsUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _roomsUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("by-accommodation/{accommodationId}")]
    public async Task<IActionResult> GetByAccommodationIdAsync(int accommodationId)
    {
        var response = await _roomsUnitOfWork.GetByAccommodationIdAsync(accommodationId);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("totalRecordsPaginated")]
    public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _roomsUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [HttpGet("avaliable")]
    public async Task<IActionResult> GetAvailableRoomsAsync()
    {
        return Ok(await _roomsUnitOfWork.GetAvailableRoomsAsync());
    }

    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(RoomCreateDTO roomCreateDTO)
    {
        var action = await _roomsUnitOfWork.UpdateAsync(roomCreateDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
}