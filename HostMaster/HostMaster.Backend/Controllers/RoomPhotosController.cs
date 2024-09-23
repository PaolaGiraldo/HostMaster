using HostMaster.Backend.UnitsOfWork.Implementations;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HostMaster.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomPhotosController : GenericController<RoomPhoto>
{
    private readonly IRoomPhotosUnitOfWork _roomPhotosUnitOfWork;

    public RoomPhotosController(IGenericUnitOfWork<RoomPhoto> unitOfWork, IRoomPhotosUnitOfWork roomPhotosUnitOfWork) : base(unitOfWork)
    {
        _roomPhotosUnitOfWork = roomPhotosUnitOfWork;
    }

    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(RoomPhotoCreateDTO roomPhotoCreateDTO)
    {
        var action = await _roomPhotosUnitOfWork.AddAsync(roomPhotoCreateDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("by-roomId/{roomId}")]
    public async Task<IActionResult> GetByAccommodationIdAsync(int roomId)
    {
        var response = await _roomPhotosUnitOfWork.GetByRoomIdAsync(roomId);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    //hide resource inhereted from genericController

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        return base.GetAsync(pagination);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAsync(int id)
    {
        return base.GetAsync(id);
    }

    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(RoomPhotoCreateDTO roomPhotoCreateDTO)
    {
        var action = await _roomPhotosUnitOfWork.UpdateAsync(roomPhotoCreateDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpDelete("by-roomId/{roomId}")]
    public async Task<IActionResult> DeleteByRoomIdAsync(int roomId)
    {
        var response = await _roomPhotosUnitOfWork.DeleteByRoomIdAsync(roomId);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }
}