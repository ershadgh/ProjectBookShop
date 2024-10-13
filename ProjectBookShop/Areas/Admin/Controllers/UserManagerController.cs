using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using NuGet.Protocol;
using ProjectBookShop.Areas.Identity.Data;
using ProjectBookShop.classes;
using ProjectBookShop.Models.ViewModels;
using ReflectionIT.Mvc.Paging;
using System.Formats.Asn1;
using System.Web.Razor.Tokenizer.Symbols;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagerController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IConvertDate _convertDatel;
        private readonly IEmailSender _emailSender;
        public UserManagerController(IApplicationUserManager userManager,IApplicationRoleManager roleManager,IConvertDate convertDate,IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _convertDatel = convertDate;
            _emailSender = emailSender;

        }

        public async Task<IActionResult> Index(string Msg, int row = 10, int page = 1)
        {

            var PagingModel = PagingList.Create(await _userManager.GetAllUsersWithRolesAsync(), row, page);
            if (Msg == "Success")
                ViewBag.Alert = "عضویت با موفقیت انجام شد";
            if(Msg=="SendEmailSuccess")
                ViewBag.Alert = "ارسال ایمیل به کاربران با موفقیت انجام شد";
 
            return View(PagingModel);
        }
        public async Task<IActionResult> Details(String ID)
        {

            if (ID == null)
                return NotFound();
            else
            {
                var User = await _userManager.FindUserWithRolesByIdAsync(ID);
                if (User == null)
                    return NotFound();
                else
                    return View(User);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UsersViewModel viewModel = new UsersViewModel();

            if (id == null)
                return NotFound();
            else
            {
                var User = await _userManager.FindUserWithRolesByIdAsync(id);
                if (User == null)
                    return NotFound();
                else
                    if(User.BirthDate!=null)
                    User.PersianBirthDate = _convertDatel.ConverMiladitoShamsi((DateTime)User.BirthDate, "yyyy/MM/dd");
                    ViewBag.AllRoles =  _roleManager.GetAllRoles();
                    return View(User);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User =await _userManager.FindByIdAsync(viewModel.Id);
                if (User==null)
                    return NotFound();
                else
                {
                    IdentityResult Result;
                    var RecentRoles =await  _userManager.GetRolesAsync(User);
                    var DeleteRoles = RecentRoles.Except(viewModel.Roles);
                    var AddRoles = viewModel.Roles.Except(RecentRoles);

                    Result=await _userManager.RemoveFromRolesAsync(User, DeleteRoles);
                    if (Result.Succeeded)
                    {
                        Result =await _userManager.AddToRolesAsync(User, AddRoles);
                        if (Result.Succeeded)
                        {
                            User.FrisName = viewModel.FirstName;
                            User.LastName = viewModel.LastName;
                            User.Email = viewModel.Email;
                            User.PhoneNumber = viewModel.PhoneNumber;
                            User.UserName = viewModel.UserName;
                            User.BirthDate = _convertDatel.ConvertShamsiToMiladi(viewModel.PersianBirthDate);

                            Result = await _userManager.UpdateAsync(User);
                            if (Result.Succeeded)
                            {
                                ViewBag.AlertSuccess = "ذخیره تغییرات با موفقیت انجام شد";
                            }
                        }
                    }
                    if(Result!=null)
                    {
                        foreach (var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
             
            }
            ViewBag.AllRoles = _roleManager.GetAllRoles();
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();
            else
            {
                var User =await _userManager.FindByIdAsync(id);
                if (User == null)
                    return NotFound();
                else
                    return View(User);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> Deleted(string id)
        {
            if (id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            else
            {
                var Result =await _userManager.DeleteAsync(User);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ViewBag.AlertError = "   در حذف اطلاعات خطایی رخ داده است";
                return View(User);

            }

        }
        public async Task<IActionResult> SendEmail(string[] emails,string subject,string message )
        {
            if(emails!=null)
            {
                for (int i = 0; i < emails.Length; i++)
                {
                   await _emailSender.SendEmailAsync(emails[i], subject, message);
                }
            }
            return RedirectToAction("Index", new { Msg = "SendEmailSuccess" });
        }
    }
}
