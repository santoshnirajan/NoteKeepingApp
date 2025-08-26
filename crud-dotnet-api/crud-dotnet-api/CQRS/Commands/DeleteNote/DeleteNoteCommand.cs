
using MediatR;

namespace crud_dotnet_api.CQRS.Commands.DeleteNote
{
    public class DeleteNoteCommand : IRequest
    {
        public Guid NoteId { get; set; }

        public DeleteNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}