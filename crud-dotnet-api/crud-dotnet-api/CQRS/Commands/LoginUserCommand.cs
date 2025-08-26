
using MediatR;
using crud_dotnet_api.Model.DTOs;
using crud_dotnet_api.Model;

namespace crud_dotnet_api.CQRS.Commands
{
    public class LoginUserCommand : IRequest<string> // Return JWT token
    {
        public LoginModel LoginModel { get; set; }
    }
}
