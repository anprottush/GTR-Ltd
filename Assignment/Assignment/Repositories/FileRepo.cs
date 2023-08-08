using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Repositories
{
    public class FileRepo
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserDbContext db;
        public FileRepo(UserDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.db = db;
        }


        public string UploadDocument(string id, FileModel fileModel)
        {
            var studentId = db.Users.FirstOrDefault(
                u =>
                u.StudentId.Equals(id));
            if (studentId == null)
            {
                return null;
            }
            else
            {
                var studentUploadsDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads", id);
                if (!Directory.Exists(studentUploadsDirectory))
                {
                    Directory.CreateDirectory(studentUploadsDirectory);
                }
                if (fileModel.FileData == null || fileModel.FileData.Length == 0)
                {
                    return null;
                }
                else
                {
                    var fileName = Path.GetFileName(fileModel.FileData.FileName);
                    var filePath = Path.Combine(studentUploadsDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileModel.FileData.CopyTo(stream);
                    }

                    var document = new Document
                    {
                        DocumentType = fileModel.DocumentType,
                        FileName = fileName,
                        StudentId = id
                    };

                    db.Documents.Add(document);
                    db.SaveChanges();
                    return filePath;
                }
            }
        }

        public FileStreamResult DownloadDocument(string id, string documenttype)
        {
            var document = db.Documents
                .FirstOrDefault(d => d.StudentId == id && 
                                d.DocumentType==documenttype);

            if (document == null)
            {
                return null;
            }
            else
            {
                var studentUploadsDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads", id);
                var filePath = Path.Combine(studentUploadsDirectory, document.FileName);

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                
                return new FileStreamResult(fileStream, "application/octet-stream")
                {
                    FileDownloadName = document.FileName
                };
            }
                

            
        }

        public bool UpdateDocument(string id, string documenttype, FileModel fileModel)
        {
            var document = db.Documents.FirstOrDefault(
                u =>
                    u.StudentId.Equals(id) &&
                    u.DocumentType.Equals(documenttype)
            );

            if (document == null)
            {
                return false;
                
            }
            else
            {
                var studentUploadsDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads", id);

                var oldFilePath = Path.Combine(studentUploadsDirectory, document.FileName);
                if (File.Exists(oldFilePath))
                    File.Delete(oldFilePath);

                var fileName = Path.GetFileName(fileModel.FileData.FileName);
                var filePath = Path.Combine(studentUploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    fileModel.FileData.CopyTo(stream);
                }

                document.DocumentType = fileModel.DocumentType;
                document.FileName = fileName;

                db.SaveChanges();
                return true;
                
            }
        }

        public bool DeleteDocument(string studentId, string documenttype)
        {
            var document = db.Documents.FirstOrDefault(
                            u =>
                                u.StudentId.Equals(studentId) && 
                                u.DocumentType.Equals(documenttype));

            if (document == null)
            {
                return false;
            }
            else
            {
                var studentUploadsDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads", studentId);
                var fileName = Path.GetFileName(document.FileName);
                var filePath = Path.Combine(studentUploadsDirectory, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    db.Documents.Remove(document);
                    db.SaveChanges();
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
