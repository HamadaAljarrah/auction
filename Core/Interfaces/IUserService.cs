

namespace DistLab2.Core.Interfaces
{
    public interface IUserService
    {
        void CreateUser(User user);
        User GetUsername(string email);
    }
}