using CloudinaryDotNet.Actions;
using KeyShop.Interfaces;
using KeyShop.Models;
using KeyShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KeyShop.Controllers {
    public class DashboardController : Controller {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository,
                                   IHttpContextAccessor httpContextAccessor, IPhotoService photoService) {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        private void MapUserEdit(User user, EditUserDashboardViewModel editVM,
                                 ImageUploadResult photoResult) {
            user.Id = editVM.Id;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }
        public async Task<IActionResult> Index() {
            var userGames = await _dashboardRepository.GetAllUserGames();
            var dashboardViewModel = new DashboardViewModel() { Games = userGames };

            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile() {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currentUserId);

            if (user == null) {
                return View("Error");
            }

            var editUserViewModel = new EditUserDashboardViewModel() {
                Id = currentUserId,
                ProfileImageUrl = user.ProfileImageUrl,
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            User user = await _dashboardRepository.GetUserByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null) {
                var photoResult = await _photoService.AddPhotoAsyns(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            } else {
                try {
                    await _photoService.DeletePhotoAsinc(user.ProfileImageUrl);
                } catch (System.Exception) {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }

                var photoResult = await _photoService.AddPhotoAsyns(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
