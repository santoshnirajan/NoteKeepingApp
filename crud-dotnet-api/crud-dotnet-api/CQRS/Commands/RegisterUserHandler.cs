
using MediatR;
using crud_dotnet_api.Model;
using Microsoft.AspNetCore.Identity;
using global::crud_dotnet_api.Model;

namespace crud_dotnet_api.CQRS.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            if (model.Password != model.ConfirmPassword)
                return false; // or throw custom exception

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded;
        }
    }
}