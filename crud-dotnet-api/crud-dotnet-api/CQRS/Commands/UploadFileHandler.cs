
using MediatR;
using crud_dotnet_api.Data;
using crud_dotnet_api.Model;
using System.IO;

namespace crud_dotnet_api.CQRS.Commands
{
    public class UploadFileHandler : IRequestHandler<UploadFileCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly string _uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public UploadFileHandler(AppDbContext context)
        {
            _context = context;
            if (!Directory.Exists(_uploadDir)) Directory.CreateDirectory(_uploadDir);
        }

        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(_uploadDir, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await request.File.CopyToAsync(stream, cancellationToken);

            var uploadedFile = new UploadedFileModel
            {
                FileName = uniqueFileName,
                FilePath = filePath,
                AltText = request.AltText,
                Description = request.Description,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.Now
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}