﻿using HostMaster.Backend.UnitsOfWork.Interfaces;
using HostMaster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HostMaster.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : GenericController<User>
{
	public UserController(IGenericUnitOfWork<User> unitOfWork) : base(unitOfWork)
	{
	}
}