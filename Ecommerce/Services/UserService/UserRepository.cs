using Ecommerce.DbContexts;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.UserService
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceContext _context;

        public UserRepository(EcommerceContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Users>> getAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<Users> UsersAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public void AddUsersAsync(Users users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            _context.Users.Add(users);
        }
        public async Task UpdateUsersAsync(Users users)
        {
            _context.Entry(users).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
