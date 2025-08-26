
using MediatR;
using crud_dotnet_api.Model.DTOs;


namespace crud_dotnet_api.CQRS.Commands.UpdateNote
{
    public class UpdateNoteCommand : IRequest
    {
        public NoteEditDTO NoteEditDTO { get; set; }

        public UpdateNoteCommand(NoteEditDTO noteEditDTO)
        {
            NoteEditDTO = noteEditDTO;
        }
    }
}
