using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Licenta.Controlers
{
    // stil work in progres
    public class AccountController : Controller
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public Boolean RememberMe { get; set; }
        public string ReturnUrl { get; set; }
       
        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signManager.PasswordSignInAsync(model.Username,
        //           model.Password, model.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //            {
        //                return Redirect(model.ReturnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //    }
        //    ModelState.AddModelError("", "Invalid login attempt");
        //    return View(model);
        //}

    }
}