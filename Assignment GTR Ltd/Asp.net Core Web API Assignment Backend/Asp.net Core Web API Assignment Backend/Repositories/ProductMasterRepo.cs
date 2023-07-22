using Asp.net_Core_Web_API_Assignment_Backend.Interfaces;
using Asp.net_Core_Web_API_Assignment_Backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Asp.net_Core_Web_API_Assignment_Backend.Repositories
{
    public class ProductMasterRepo : IProductRepo<ProductMaster, int, bool>
    {
        private readonly UserDbContext db;
        public ProductMasterRepo(UserDbContext db) 
        {
            this.db = db;
        }
   
        public bool DeleteProduct(int id)
        {
            db.Database.ExecuteSqlRaw("EXEC sp_RemoveProduct @Id", new SqlParameter("@Id", id));
            return true;
        }

        public IEnumerable<ProductMaster> GetAllProducts()
        {
            var product = db.ProductMasters.FromSqlRaw("EXEC sp_FindAllProducts").AsEnumerable();
            if (product != null)
            {
                return product;
            }

            else
            {
                return null;
            }
        }

        public ProductMaster GetProduct(int id)
        {
            var product = db.ProductMasters.FromSqlRaw("EXEC sp_FindProduct @Id", new SqlParameter("@Id", id)).AsEnumerable().FirstOrDefault();
            if (product != null)
            {
                return product;
            }
           
            else
            {
                return null;
            }
        }

        public bool InsertProduct(ProductMaster product)
        {
            db.Database.ExecuteSqlRaw("EXEC sp_CreateProduct @Name, @Barcode, @Type, @Status, @Price",
           new SqlParameter("@Name", product.Name),
           new SqlParameter("@Barcode", product.Barcode),
           new SqlParameter("@Type", product.Type),
           new SqlParameter("@Status", product.Quantity),
           new SqlParameter("@Price", product.Price));

            db.ProductDetails.Add(new ProductDetail());
            return true;

        }

        public bool UpdateProduct(ProductMaster product)
        {
            db.Database.ExecuteSqlRaw("EXEC sp_EditProduct @Id, @Name, @Barcode, @Type, @Status, @Price",
            new SqlParameter("@Id", product.Id),
            new SqlParameter("@Name", product.Name),
            new SqlParameter("@Barcode", product.Barcode),
           new SqlParameter("@Type", product.Type),
           new SqlParameter("@Status", product.Quantity),
           new SqlParameter("@Price", product.Price));
            return true;
        }
    }
}
