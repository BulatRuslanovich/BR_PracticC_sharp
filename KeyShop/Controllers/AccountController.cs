using KeyShop.Data;
using KeyShop.Models;
using KeyShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KeyShop.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDBContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
                                 AppDBContext context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) {
            if (!ModelState.IsValid) {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null) {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck) {
                    var result =
                        await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded) {
                        return RedirectToAction("Index", "Game");
                    }
                }

                TempData["Error"] = "Wrong, try again!";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong, try again!";
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register() {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) {
            if (!ModelState.IsValid) {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null) {
                TempData["Error"] = "This email address is already is use";
                return View(registerViewModel);
            }

            var newUser = new User() { Email = registerViewModel.EmailAddress,
                                       UserName = registerViewModel.EmailAddress, Balance = 0 };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded) {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            } else {
                TempData["Error"] = newUserResponse.Errors.FirstOrDefault().Description;
                return View(registerViewModel);
            }

            return RedirectToAction("Index", "Game");
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Game");
        }
    }
}
