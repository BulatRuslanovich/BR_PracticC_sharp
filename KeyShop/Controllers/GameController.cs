using KeyShop.Interfaces;
using KeyShop.Models;
using KeyShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KeyShop.Controllers {
    public class GameController : Controller {
        private readonly IGameRepository _gameRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GameController(IGameRepository gameRepository, IPhotoService photoService,
                              IHttpContextAccessor httpContextAccessor) {
            _gameRepository = gameRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index() {
            var games = await _gameRepository.GetAll();
            return View(games);
        }

        public async Task<IActionResult> Detail(int id) {
            Game game = await _gameRepository.GetByIdAsync(id);
            return View(game);
        }

        // public async Task<IActionResult> AddToBasket(int id) {
        // 	Game game = await _gameRepository.GetByIdAsync(id);
        // 	var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        // 	var createGameViewModel = new CreateGameViewModel {UserId = currentUserId};

        // 	return View();
        // }

        public IActionResult Create() {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createGameViewModel = new CreateGameViewModel { UserId = currentUserId };

            return View(createGameViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameViewModel gameVM) {
            if (ModelState.IsValid) {
                // var results = new List<ImageUploadResult>();
                // foreach (var image in gameVM.Images) {
                // 	var result = await _photoService.AddPhotoAsyns(image);
                // 	results.Add(result);
                // }

                // TODO : исправить это недоразумение
                var result_0 = await _photoService.AddPhotoAsyns(gameVM.MainImage);
                var result_1 = await _photoService.AddPhotoAsyns(gameVM.Image_1);
                var result_2 = await _photoService.AddPhotoAsyns(gameVM.Image_2);
                var result_3 = await _photoService.AddPhotoAsyns(gameVM.Image_3);
                var result_4 = await _photoService.AddPhotoAsyns(gameVM.Image_4);

                var game = new Game { Title = gameVM.Title,
                                      Description = gameVM.Description,
                                      Publisher = gameVM.Publisher,
                                      Developer = gameVM.Developer,
                                      Platform = gameVM.Platform,
                                      Genre = gameVM.Genre,
                                      Price = gameVM.Price,
                                      UserId = gameVM.UserId,
                                      Images = { result_0.Url.ToString(), result_1.Url.ToString(),
                                                 result_2.Url.ToString(), result_3.Url.ToString(),
                                                 result_4.Url.ToString() } };
                _gameRepository.Add(game);
                return RedirectToAction("Index");
            } else {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(gameVM);
        }

        public async Task<IActionResult> Edit(int id) {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null) {
                return View("Error");
            }

            var gameVM = new EditGameViewModel { Title = game.Title,         Description = game.Description,
                                                 Publisher = game.Publisher, Developer = game.Developer,
                                                 Platform = game.Platform,   Genre = game.Genre,
                                                 Price = game.Price,         URLs = game.Images };

            return View(gameVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGameViewModel gameVM) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", gameVM);
            }

            var userGame = await _gameRepository.GetByIdAsyncNoTracking(id);

            if (userGame != null) {
                try {
                    foreach (var img in userGame.Images) {
                        await _photoService.DeletePhotoAsinc(img);
                    }
                } catch (System.Exception) {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(userGame);
                }

                var result_0 = await _photoService.AddPhotoAsyns(gameVM.MainImage);
                var result_1 = await _photoService.AddPhotoAsyns(gameVM.Image_1);
                var result_2 = await _photoService.AddPhotoAsyns(gameVM.Image_2);
                var result_3 = await _photoService.AddPhotoAsyns(gameVM.Image_3);
                var result_4 = await _photoService.AddPhotoAsyns(gameVM.Image_4);

                var game = new Game { Id = id,
                                      Title = gameVM.Title,
                                      Description = gameVM.Description,
                                      Publisher = gameVM.Publisher,
                                      Developer = gameVM.Developer,
                                      Platform = gameVM.Platform,
                                      Genre = gameVM.Genre,
                                      Price = gameVM.Price,
                                      Images = { result_0.Url.ToString(), result_1.Url.ToString(),
                                                 result_2.Url.ToString(), result_3.Url.ToString(),
                                                 result_4.Url.ToString() } };

                _gameRepository.Update(game);
                return RedirectToAction("Index");
            } else {
                return View(gameVM);
            }
        }

        public async Task<IActionResult> Delete(int id) {
            var gameDetails = await _gameRepository.GetByIdAsync(id);
            if (gameDetails == null) {
                return View("Error");
            }

            return View(gameDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGame(int id) {
            var gameDetails = await _gameRepository.GetByIdAsync(id);
            if (gameDetails == null) {
                return View("Error");
            }

            _gameRepository.Delete(gameDetails);
            return RedirectToAction("Index");
        }
    }
}
