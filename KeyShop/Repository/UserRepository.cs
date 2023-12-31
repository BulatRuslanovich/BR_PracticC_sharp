using KeyShop.Data;
using KeyShop.Interfaces;
using KeyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyShop.Repository {
    public class UserRepository : IUserRepository {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context) {
            _context = context;
        }
        public bool Add(User user) {
            throw new NotImplementedException();
        }

        public bool Delete(User user) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers() {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id) {
            return await _context.Users.FindAsync(id);
        }

        public bool Save() {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user) {
            _context.Update(user);
            return Save();
        }
    }
}
