
using MediatR;
using crud_dotnet_api.Model.DTOs;


namespace crud_dotnet_api.CQRS.Commands.CreateNote
{
    public class CreateNoteCommand : IRequest
    {
        public NoteDTO NoteDTO { get; set; }

        public CreateNoteCommand(NoteDTO noteDTO)
        {
            NoteDTO = noteDTO;
        }
    }
}
