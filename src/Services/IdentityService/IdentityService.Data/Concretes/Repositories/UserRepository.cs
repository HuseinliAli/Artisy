using IdentityService.Data.Abstracts;
using IdentityService.Data.Concretes.Contexts;
using IdentityService.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityService.Data.Concretes.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;
        private readonly DbSet<User> _users;
        public UserRepository(IdentityDbContext context)
        {
            _context = context;
            _users = _context.Set<User>();
        }
        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetWhere(Expression<Func<User, bool>> predicate = null)
            => predicate == null ? _users : _users.Where(predicate);

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync(); 
    }
}
