using MediatR;
using Microsoft.AspNetCore.Http;

namespace crud_dotnet_api.CQRS.Commands
{
    public class UploadFileCommand : IRequest<Unit>
    {
        public IFormFile File { get; set; }
        public string AltText { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
    }
}