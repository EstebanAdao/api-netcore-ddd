using Api.Domain.Entities;
using Domain.Interfaces.Services.User;
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
		[HttpPost]
		public async Task<object> Login([FromBody] UserEntity userEntity, [FromServices] ILoginService service)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (userEntity == null)
			{
				return BadRequest();
			}

			try
			{
				var result = await service.FindByLogin(userEntity);
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
