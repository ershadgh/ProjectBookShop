using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ProjectBookShop.Areas.Identity.Data;
using ProjectBookShop.Models.ViewModels;
using System.Text.Encodings.Web;

namespace ProjectBookShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _usertManager;
        private readonly IEmailSender  _emailSender;
        public AccountController(IApplicationUserManager usertManager, IApplicationRoleManager roleManger, IEmailSender emailSender)
        {
            _roleManager = roleManger;
            _usertManager = usertManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.UserName, Email = viewModel.Email, PhoneNumber = viewModel.PhoneNumber, RegisterDate = DateTime.Now, IsActive = true };
                IdentityResult result = await _usertManager.CreateAsync(user,viewModel.Password);
                if(result.Succeeded)
                {
                    var role =await _roleManager.FindByNameAsync("کاربر");
                    if (role == null)
                       await  _roleManager.CreateAsync(new ApplicationRole ("کاربر","کاربر"));

                    result = await _usertManager.AddToRoleAsync(user, "کاربر");
                    if(result.Succeeded)
                    {
                        var code =await _usertManager.GenerateEmailConfirmationTokenAsync(user);
                        var CallbackUrl = Url.Action("ConfirmEmail", "Home", values: new { userId = user.Id, code = code });
                        await _emailSender.SendEmailAsync(user.Email, "تاییذ ایمیل حساب کابری", $"<div dir='rtl' style='font-family;tahoma;font-size:14px'>لطفا با کلیک روی لینک رویه رو ایمیل خود را تایید کنید.<a href={HtmlEncoder.Default.Encode(CallbackUrl)}>کلیک کنید</a></div>");
                        return RedirectToAction("Index", "Home", new { id = "ConfirmEmail" });
                    }

                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }

            }
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            if (userId==null || code==null)
                return RedirectToAction("Index", "Home");

            var user = await _usertManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound($"Unable to load user with ID `{userId}` ");

            var result = await _usertManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Error confirming email for user with ID `{userId}`");

            return View();


            
        }
    }
}
