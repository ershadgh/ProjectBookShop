using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ProjectBookShop.Areas.Identity.Data;
using ProjectBookShop.classes;
namespace ProjectBookShop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IApplicationRoleManager _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public IEnumerable<ApplicationRole> GetRoles { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        [BindProperty]
        public string[]? UserRoles { get; set; }

        public class InputModel
        {
           
            [Required(ErrorMessage = ("وارد نمودن {0} الزامی است. "))]
            [EmailAddress(ErrorMessage = ("ایمیل شما نا معتبر است"))]
            [Display(Name = "ایمیل")]
            public string? Email { get; set; }

            [Required(ErrorMessage = ("وارد نمودن {0} الزامی است."))]
            [StringLength(100, ErrorMessage = "{0}باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد. ", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار کلمه عبور")]
            [Compare("Password", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد")]
            public string? ConfirmPassword { get; set; }

            [Display(Name = "نام")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است")]
            public string? Name { get; set; }
            [Display(Name = "نام خانوادگی")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string? Family { get; set; }
            [Display(Name = "تاریخ تولد")]
            [Required(ErrorMessage = "وارد نمودند {0} الزامی است.")]
            public string? BirthDate { get; set; }
            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = "وارد نمودن {0} نام کاربری الزامی است.")]
            public string? UserName { get; set; }
            [Display(Name = "شماره موبایل")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string? PhoneNumber { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
           GetRoles= _roleManager.GetAllRoles();

            ReturnUrl = returnUrl;
            // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Admin/UserManager/Index?Msg=Success");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                ConvertDate convertDate = new ConvertDate();
               
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, FrisName=Input.Name,LastName=Input.Family,PhoneNumber=Input.PhoneNumber,BirthDate= convertDate.ConvertShamsiToMiladi(Input.BirthDate),IsActive=true,RegisterDate=DateTime.Now};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if (UserRoles != null)
                    {
                        await _userManager.AddToRolesAsync(user, UserRoles);
                        
                        return LocalRedirect(returnUrl);
                    }
                   
                    //   var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //  code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    // var callbackUrl = Url.Page(
                    //   "/Account/ConfirmEmail",
                    // pageHandler: null,
                    //values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    // protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            GetRoles = _roleManager.GetAllRoles();
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
