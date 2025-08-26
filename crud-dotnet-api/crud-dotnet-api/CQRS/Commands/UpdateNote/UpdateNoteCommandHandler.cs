
using MediatR;
using crud_dotnet_api.Data;
using Microsoft.EntityFrameworkCore;


namespace crud_dotnet_api.CQRS.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly AppDbContext _context;

        public UpdateNoteCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.FindAsync(new object[] { request.NoteEditDTO.Id }, cancellationToken);
            if (note == null)
                throw new Exception("Note not found.");

            note.Title = request.NoteEditDTO.Title;
            note.Content = request.NoteEditDTO.Content;
            note.Label = request.NoteEditDTO.Label;
            note.BackgroundColor = request.NoteEditDTO.BackgroundColor;
            note.FontColor = request.NoteEditDTO.FontColor;
            note.ModifiedOn = DateTime.Now;

            _context.Notes.Update(note);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
