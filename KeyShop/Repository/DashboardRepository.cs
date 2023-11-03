using KeyShop.Data;
using KeyShop.Interfaces;
using KeyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyShop.Repository {
    public class DashboardRepository : IDashboardRepository {
        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(AppDBContext context, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Game>> GetAllUserGames() {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userGames = _context.Games.Where(g => g.User.Id == currentUser.ToString());
            return userGames.ToList();
        }

        public async Task<User> GetUserById(string id) {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByIdNoTracking(string id) {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(User user) {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save() {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
