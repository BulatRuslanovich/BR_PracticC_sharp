using Microsoft.AspNetCore.Identity;

namespace KeyShop.Models {
    public class User : IdentityUser {
        public double? Balance { get; set; }
        public string? ProfileImageUrl { get; set; }
        public List<string> GameIdList { get; set; } = new List<string>();
    }
}
