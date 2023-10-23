

namespace DistLab2.Core.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user,bool login);
        User GetCurrnetUser();
        Task<bool> LoginUser(User user);
        void LogoutUser();

        bool IsAuthenticated();
        Task<bool> IsAdmin();

        IEnumerable<User> GetNoneAdminUsers();

        void DeleteUser(string email);
        public User GetUserByEmail(string email);

        void UpdateUsername(string email, string username);


    }
}