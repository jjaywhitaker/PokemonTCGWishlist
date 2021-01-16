using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokemonTCGWishlist.Areas.Identity.Data;
using PokemonTCGWishlist.Models;

namespace PokemonTCGWishlist.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(UserManager<PokemonTCGWishlistUser> userManager, SignInManager<PokemonTCGWishlistUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<PokemonTCGWishlistUser> UserManager { get; }
        public SignInManager<PokemonTCGWishlistUser> SignInManager { get; }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new PokemonTCGWishlistUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                   await  SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
