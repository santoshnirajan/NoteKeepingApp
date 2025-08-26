using MediatR;
using crud_dotnet_api.Data;
using crud_dotnet_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using crud_dotnet_api.CQRS.Queries;

namespace crud_dotnet_api.Features.Files.Queries
{
    public class GetFilesByUserHandler : IRequestHandler<GetFilesByUserQuery, List<UploadedFileModel>>
    {
        private readonly AppDbContext _context;
        public GetFilesByUserHandler(AppDbContext context) => _context = context;

        public async Task<List<UploadedFileModel>> Handle(GetFilesByUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.UploadedFiles
                .Where(f => f.CreatedBy == request.CreatedBy)
                .ToListAsync(cancellationToken);
        }
    }
}