using KeyShop.Data.Enum;

namespace KeyShop.ViewModels {
    public class EditGameViewModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public Platform Platform { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public List<string> URLs { get; set; } = new List<string>();
        public IFormFile MainImage { get; set; }
        public IFormFile Image_1 { get; set; }
        public IFormFile Image_2 { get; set; }
        public IFormFile Image_3 { get; set; }
        public IFormFile Image_4 { get; set; }
    }
}
