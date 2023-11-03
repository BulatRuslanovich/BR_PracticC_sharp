using System.ComponentModel.DataAnnotations.Schema;
using KeyShop.Data.Enum;

namespace KeyShop.Models {
    public class Game {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public Platform Platform { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
