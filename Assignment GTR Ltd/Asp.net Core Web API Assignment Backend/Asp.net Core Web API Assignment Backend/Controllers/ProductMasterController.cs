using Asp.net_Core_Web_API_Assignment_Backend.Models;
using Asp.net_Core_Web_API_Assignment_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_Core_Web_API_Assignment_Backend.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMasterController : ControllerBase
    {
        
        private readonly UserDbContext db;
        public ProductMasterController(UserDbContext db)
        {
            this.db = db;
        }
        //[Authorize]
        [HttpGet("all")]
        public ActionResult GetAll()
        {
            try
            {
                var products = new ProductMasterRepo(db).GetAllProducts();
                return Ok(new { Success = "true", StatusCode = 200, Payload = products});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var product = new ProductMasterRepo(db).GetProduct(id);

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public ActionResult AddProduct([FromBody] ProductMaster product)
        {
            try
            {
                new ProductMasterRepo(db).InsertProduct(product);
                return Ok(new { Message = "Product created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize]
        [HttpPost("update/{id}")]
        public ActionResult Update(int id, [FromBody] ProductMaster product)
        {
            try
            {
                var existingProduct = new ProductMasterRepo(db).GetProduct(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                else
                {
                    product.Id = id;
                    new ProductMasterRepo(db).UpdateProduct(product);
                    return Ok(new { Message = "Product updated successfully!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        //[Authorize]
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var existingProduct = new ProductMasterRepo(db).GetProduct(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                else
                {
                    var data = new ProductMasterRepo(db).DeleteProduct(id);
                    return Ok(new { Success = "true", Message = "Product deleted successfully!", Payload = data });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
