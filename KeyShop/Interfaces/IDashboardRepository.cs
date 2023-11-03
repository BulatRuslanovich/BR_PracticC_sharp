using KeyShop.Models;

namespace KeyShop.Interfaces {
    public interface IDashboardRepository {
        Task<List<Game>> GetAllUserGames();
        Task<User> GetUserById(string id);
        Task<User> GetUserByIdNoTracking(string id);
        bool Update(User user);
        bool Save();
    }
}
