namespace Asp.net_Core_Web_API_Assignment_Backend.Interfaces
{
    public interface IAuth<TClass>
    {
        TClass Authenticate(string username, string password);

    }
}
