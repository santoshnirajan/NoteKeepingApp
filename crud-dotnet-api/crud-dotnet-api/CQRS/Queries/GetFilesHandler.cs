using MediatR;
using crud_dotnet_api.Data;
using crud_dotnet_api.Model;
using Microsoft.EntityFrameworkCore;

namespace crud_dotnet_api.CQRS.Queries
{
    public class GetFilesHandler : IRequestHandler<GetFilesQuery, List<UploadedFileModel>>
    {
        private readonly AppDbContext _context;
        public GetFilesHandler(AppDbContext context) => _context = context;

        public async Task<List<UploadedFileModel>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            return await _context.UploadedFiles.ToListAsync(cancellationToken);
        }
    }
}