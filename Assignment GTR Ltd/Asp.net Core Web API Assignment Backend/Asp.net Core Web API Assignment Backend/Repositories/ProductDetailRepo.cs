using Asp.net_Core_Web_API_Assignment_Backend.Interfaces;
using Asp.net_Core_Web_API_Assignment_Backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_Core_Web_API_Assignment_Backend.Repositories
{
    public class ProductDetailRepo : IProductRepo<ProductDetail, int, bool>
    {
        private readonly UserDbContext db;
        public ProductDetailRepo(UserDbContext db)
        {
            this.db = db;
        }

        public bool DeleteProduct(int id)
        {
            db.Database.ExecuteSqlRaw("EXEC sp_DeleteProduct @Id", new SqlParameter("@Id", id));
            return true;
        }

        public IEnumerable<ProductDetail> GetAllProducts()
        {
            var product = db.ProductDetails.FromSqlRaw("EXEC sp_GetAllProducts").AsEnumerable();
            if (product != null)
            {
                return product;
            }

            else
            {
                return null;
            }
        }

        public ProductDetail GetProduct(int id)
        {
            var product = db.ProductDetails.FromSqlRaw("EXEC sp_GetProduct @Id", new SqlParameter("@Id", id)).AsEnumerable().FirstOrDefault();
            if (product != null)
            {
                return product;
            }

            else
            {
                return null;
            }
        }

        public bool InsertProduct(ProductDetail product)
        {
            db.Database.ExecuteSqlRaw("EXEC sp_InsertProduct @PurchaseCustomer, @PurchaseDate, @SalesEmployee, @Description, @Stock, @Status, @ProductMasterId",
           new SqlParameter("@PurchaseCustomer", product.PurchaseCustomer),
           new SqlParameter("@PurchaseDate", product.PurchaseDate),
           new SqlParameter("@SalesEmployee", product.SalesEmployee),
           new SqlParameter("@Description", product.Description),
           new SqlParameter("@Stock", product.Stock),
           new SqlParameter("@Status", product.Status),
           new SqlParameter("@ProductMasterId", product.ProductMasterId)

           );

            return true;

        }

        public bool UpdateProduct(ProductDetail product)
        {
            db.Database.ExecuteSqlRaw("EXEC sp_UpdateProduct @Id, @PurchaseCustomer, @PurchaseDate, @SalesEmployee, @Description, @Stock, @Status, @ProductMasterId",
            new SqlParameter("@Id", product.Id),
             new SqlParameter("@PurchaseCustomer", product.PurchaseCustomer),
           new SqlParameter("@PurchaseDate", product.PurchaseDate),
           new SqlParameter("@SalesEmployee", product.SalesEmployee),
           new SqlParameter("@Description", product.Description),
           new SqlParameter("@Stock", product.Stock),
           new SqlParameter("@Status", product.Status),
           new SqlParameter("@ProductMasterId", product.ProductMasterId));
            return true;
        }
    }
}
