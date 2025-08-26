namespace crud_dotnet_api.Model.DTOs
{
    public class NoteEditDTO
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackgroundColor { get; set; }
        public string FontColor { get; set; }
    }
}
