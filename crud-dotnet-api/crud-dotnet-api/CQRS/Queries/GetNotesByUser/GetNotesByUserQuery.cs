
using MediatR;
using crud_dotnet_api.Model;


namespace crud_dotnet_api.CQRS.Queries.GetNotesByUser
{
    public class GetNotesByUserQuery : IRequest<IEnumerable<NoteModel>>
    {
        public Guid UserId { get; set; }

        public GetNotesByUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
