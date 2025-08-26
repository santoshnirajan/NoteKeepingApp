
using MediatR;
using crud_dotnet_api.Model.DTOs;
using global::crud_dotnet_api.Model.DTOs;

namespace crud_dotnet_api.CQRS.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public UserRegistrationDTO Model { get; set; }
    }
}