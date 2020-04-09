using Api.Domain.Dtos;
using Api.Domain.Entities;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase
	{
		[AllowAnonymous]
		[HttpPost]
		public async Task<object> Login([FromBody] LoginDto loginDto, [FromServices] ILoginService service)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (loginDto == null)
			{
				return BadRequest();
			}

			try
			{
				var result = await service.FindByLogin(loginDto);
				if (result == null)
					return BadRequest();
				else
					return Ok(result);
			}
			catch (ArgumentException e)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
			}
		}
	}
}
