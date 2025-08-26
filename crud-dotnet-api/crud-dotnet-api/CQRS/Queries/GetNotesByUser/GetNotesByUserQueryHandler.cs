
using MediatR;
using crud_dotnet_api.Data;
using crud_dotnet_api.Model;
using Microsoft.EntityFrameworkCore;

namespace crud_dotnet_api.CQRS.Queries.GetNotesByUser
{
    public class GetNotesByUserQueryHandler : IRequestHandler<GetNotesByUserQuery, IEnumerable<NoteModel>>
    {
        private readonly AppDbContext _context;

        public GetNotesByUserQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NoteModel>> Handle(GetNotesByUserQuery request, CancellationToken cancellationToken)
        {
            var notes = await _context.Notes
                .Where(n => n.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return notes;
        }
    }
}