using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.net_Core_Web_API_Assignment_Backend.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string PurchaseCustomer { get; set; }
        public string PurchaseDate { get; set; }
        public string SalesEmployee { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Status { get; set; }
        [ForeignKey("ProductMaster")]
        public int ProductMasterId { get; set; } 
        //public virtual ProductMaster ProductMaster { get; set; }
    }
}
