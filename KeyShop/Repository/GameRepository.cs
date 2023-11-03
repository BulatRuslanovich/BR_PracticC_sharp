using KeyShop.Data;
using KeyShop.Data.Enum;
using KeyShop.Interfaces;
using KeyShop.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyShop.Repository {
    public class GameRepository : IGameRepository {
        private readonly AppDBContext _context;
        public GameRepository(AppDBContext context) {
            _context = context;
        }
        public bool Add(Game game) {
            _context.Add(game);
            return Save();
        }

        public bool Delete(Game game) {
            _context.Remove(game);
            return Save();
        }

        public async Task<IEnumerable<Game>> GetAll() {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id) {
            return await _context.Games.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Game> GetByIdAsyncNoTracking(int id) {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Game>> GetGameByGenre(Genre genre) {
            return await _context.Games.Where(c => c.Genre == genre).ToArrayAsync();
        }

        public async Task<IEnumerable<Game>> GetGameByPlatform(Platform platform) {
            return await _context.Games.Where(c => c.Platform == platform).ToArrayAsync();
        }

        public bool Save() {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Game game) {
            _context.Update(game);
            return Save();
        }
    }
}
