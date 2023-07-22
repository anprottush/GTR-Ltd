namespace Asp.net_Core_Web_API_Assignment_Backend.Models
{
    public class ProductMaster
    {
        //public ProductMaster()
        //{
            //this.ProductDetails = new HashSet<ProductDetail>();
        //}
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        //public virtual List<ProductDetail> ProductDetails { get; set; }
    }
}
