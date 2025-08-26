namespace crud_dotnet_api.Model.DTOs
{
    public class NoteDTO
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackgroundColor { get; set; }
        public string FontColor { get; set; }

        // New properties for file handling
        public string? FilePath { get; set; } // Store the path where the file is saved on the server
    }

}
