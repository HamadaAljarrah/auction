

namespace DistLab2.Core.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User GetCurrnetUser();
        Task<bool> LoginUser(User user);
        void LogoutUser();

        bool IsAuthenticated();
    }
}