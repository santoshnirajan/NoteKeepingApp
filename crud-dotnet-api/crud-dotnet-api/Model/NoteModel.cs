namespace crud_dotnet_api.Model
{
    public class NoteModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string Content { get; set; }
        public string BackgroundColor { get; set;}
        public string FontColor { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public ApplicationUser User { get; set; }   
        public Guid UserId { get; set; }

    }
}
