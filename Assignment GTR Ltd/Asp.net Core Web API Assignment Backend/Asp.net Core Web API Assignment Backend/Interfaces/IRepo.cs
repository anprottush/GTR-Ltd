namespace Asp.net_Core_Web_API_Assignment_Backend.Interfaces
{
    public interface IRepo<TClass, TId, TResult>
    {
        IEnumerable<TClass> GetAll();
        TClass Get(TId id);
        TResult Add(TClass obj);
        TResult Delete(TId id);
        TResult Update(TClass obj);
    }
}
