
using Microsoft.AspNetCore.Mvc;
using MediatR;
using crud_dotnet_api.CQRS.Commands;
using crud_dotnet_api.CQRS.Queries;

namespace crud_dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
            if (!Directory.Exists(_uploadDirectory))
                Directory.CreateDirectory(_uploadDirectory);
        }

        [HttpPost("UploadNote")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string altText, [FromForm] string description, [FromForm] Guid createdBy)
        {
            await _mediator.Send(new UploadFileCommand
            {
                File = file,
                AltText = altText,
                Description = description,
                CreatedBy = createdBy
            });

            return Ok();
        }

        [HttpGet("GetFiles")]
        public async Task<IActionResult> GetFiles()
        {
            var files = await _mediator.Send(new GetFilesQuery());
            return Ok(files.Select(f => new
            {
                f.FileName,
                f.FilePath,
                Url = Url.Content($"~/UploadedFiles/{f.FileName}"),
                f.AltText,
                f.Description,
                f.CreatedBy
            }));
        }

        [HttpGet("GetFilesByUser/{createdBy}")]
        public async Task<IActionResult> GetFilesByUser(Guid createdBy)
        {
            var files = await _mediator.Send(new GetFilesByUserQuery { CreatedBy = createdBy });
            if (!files.Any()) return NotFound($"No files found for user {createdBy}");

            return Ok(files.Select(f => new
            {
                f.FileName,
                f.FilePath,
                Url = Url.Content($"~/UploadedFiles/{f.FileName}"),
                f.AltText,
                f.Description,
                f.CreatedBy
            }));
        }

        [HttpGet("DownloadFiles")]
        public IActionResult DownloadFile([FromQuery] string path)
        {
            var fullPath = Path.Combine(_uploadDirectory, path);

            if (System.IO.File.Exists(fullPath))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(fullPath, FileMode.Open))
                    stream.CopyTo(memory);

                memory.Position = 0;
                var fileName = Path.GetFileName(fullPath);
                return File(memory, "application/octet-stream", fileName);
            }
            return NotFound();
        }
    }
}
