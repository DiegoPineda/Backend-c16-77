using Ecommerce.Entities;

namespace Ecommerce.Services.UserService
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> getAllUsersAsync();
        Task<Users> UsersAsync(int id);
        Task AddUsersAsync(Users users);
        Task UpdateUsersAsync(Users users);

    }
}
