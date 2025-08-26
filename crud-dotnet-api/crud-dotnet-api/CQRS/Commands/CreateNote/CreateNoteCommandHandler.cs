
using MediatR;
using crud_dotnet_api.Data;
using crud_dotnet_api.Model;

namespace crud_dotnet_api.CQRS.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand>
    {
        private readonly AppDbContext _context;

        public CreateNoteCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new NoteModel
            {
                UserId = request.NoteDTO.UserId,
                Title = request.NoteDTO.Title,
                Label = request.NoteDTO.Label,
                Content = request.NoteDTO.Content,
                BackgroundColor = request.NoteDTO.BackgroundColor,
                FontColor = request.NoteDTO.FontColor,
                CreatedOn = DateTime.Now
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
