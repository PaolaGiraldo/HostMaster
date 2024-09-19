using HostMaster.Backend.UnitsOfWork.Implementations;
using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.DTOs;
using HostMaster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HostMaster.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomTypesController : GenericController<RoomType>
{
    private readonly IRoomTypesUnitOfWork _roomTypesUnitOfWork;

    public RoomTypesController(IGenericUnitOfWork<RoomType> unitOfWork, IRoomTypesUnitOfWork roomTypesUnitOfWork) : base(unitOfWork)
    {
        _roomTypesUnitOfWork = roomTypesUnitOfWork;
    }

}