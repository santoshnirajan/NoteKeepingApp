using System.ComponentModel.DataAnnotations;

namespace crud_dotnet_api.Model
{
    public class UploadedFileModel
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string AltText { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
