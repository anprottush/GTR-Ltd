using Asp.net_Core_Web_API_Assignment_Backend.Models;

namespace Asp.net_Core_Web_API_Assignment_Backend.Interfaces
{
    public interface IProductRepo<TClass, TId, TResult>
    {
        TResult InsertProduct(TClass obj);
        TResult UpdateProduct(TClass obj);
        TResult DeleteProduct(TId id);
        IEnumerable<TClass> GetAllProducts();
        TClass GetProduct(TId id);
    }
}
