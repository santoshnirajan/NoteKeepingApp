using Microsoft.AspNetCore.Identity;

namespace crud_dotnet_api.Model
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<NoteModel> Notes { get; set; }   
    }
}
