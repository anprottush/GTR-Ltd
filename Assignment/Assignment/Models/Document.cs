using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Document
    {
        //public Document() {
                //this.FileModel = new List<FileModel>();
        //}
        [Key]
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string StudentId { get; set; }
        //public List<FileModel> FileModel { get; set; }= new List<FileModel>();
    }
}
