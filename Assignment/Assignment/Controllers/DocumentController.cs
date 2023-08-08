using Assignment.Models;
using Assignment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [RequestSizeLimit(104857600)]
    public class DocumentController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserDbContext db;
        public DocumentController(UserDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.db = db;
        }

        [HttpPost("upload")]
        
        public ActionResult Upload(string studentId, [FromForm] FileModel fileModel)
        {
            try
            {
                if (fileModel.FileData == null || fileModel.FileData.Length == 0)
                {
                    return BadRequest("File is required");
                }
                else
                {
                    var filePath = new FileRepo(db, webHostEnvironment).UploadDocument(studentId, fileModel);
                    if (filePath != null)
                    {
                        return Ok(new { Message = "Document uploaded successfully!", FilePath = filePath });
                    }
                    else
                    {
                        return Unauthorized("Upload failed id not exists");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpGet("download/{id}")]
        
        public ActionResult Download(string id,string documenttype)
        {
           try
           {
                var downloadfile = new FileRepo(db, webHostEnvironment).DownloadDocument(id,documenttype);
                if (downloadfile != null)
                {
                    return downloadfile;
                }
                else
                {
                    return Unauthorized("Document download failed id is not exists");
                }
            }
           catch (Exception ex)
           {
                return BadRequest(ex.Message);
           }
            
            
        }
        
        [HttpPut("update/{id}")]
        public ActionResult Update(string id, string documenttype, [FromForm] FileModel fileModel)
        {
            try
            {
                var updatefile = new FileRepo(db, webHostEnvironment).UpdateDocument(id, documenttype, fileModel);
                if (updatefile == true)
                {
                    return Ok("Document updated successful");
                }
                else
                {
                    return NotFound("Update failed document not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public ActionResult Delete(string studentId, string documenttype)
        {
            try
            {
                var deletefile = new FileRepo(db, webHostEnvironment).DeleteDocument(studentId, documenttype);

                if (deletefile == false)
                {
                    return NotFound("Document not found.");
                }
                else
                {
                    return Ok("Document deleted successfully!");

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}

