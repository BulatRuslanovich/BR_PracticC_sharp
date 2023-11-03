using KeyShop.Data.Enum;
using KeyShop.Models;

namespace KeyShop.Interfaces {
    public interface IGameRepository {
        Task<IEnumerable<Game>> GetAll();
        Task<Game> GetByIdAsync(int id);
        Task<IEnumerable<Game>> GetGameByGenre(Genre genre);
        Task<IEnumerable<Game>> GetGameByPlatform(Platform platform);
        Task<Game> GetByIdAsyncNoTracking(int id);
        bool Add(Game game);
        bool Delete(Game game);
        bool Update(Game game);
        bool Save();
    }
}
