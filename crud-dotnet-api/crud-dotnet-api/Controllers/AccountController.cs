using crud_dotnet_api.Model;
using crud_dotnet_api.Model.DTOs;
using crud_dotnet_api.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using MediatR;
using crud_dotnet_api.CQRS.Commands;

namespace crud_dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new RegisterUserCommand { Model = model });

            if (result)
                return Ok(true);

            return BadRequest("Registration failed. Check input or user may already exist.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var token = await _mediator.Send(new LoginUserCommand { LoginModel = login });

            if (token == null) return Unauthorized();

            return Ok(new { token });
        }
    }
}
