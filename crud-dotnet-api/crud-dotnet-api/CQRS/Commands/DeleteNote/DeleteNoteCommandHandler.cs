
using MediatR;
using crud_dotnet_api.Data;
using Microsoft.EntityFrameworkCore;

namespace crud_dotnet_api.CQRS.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly AppDbContext _context;

        public DeleteNoteCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == request.NoteId, cancellationToken);
            if (note == null)
                throw new Exception("Note not found.");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
