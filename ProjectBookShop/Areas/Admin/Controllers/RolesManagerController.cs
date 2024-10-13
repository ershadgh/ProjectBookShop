using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectBookShop.Areas.Identity.Data;
using ProjectBookShop.Models.ViewModels;
using ReflectionIT.Mvc.Paging;
using System.Globalization;
using System.Text;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesManagerController : Controller
    {

        private readonly IApplicationRoleManager _roleManager;

        public RolesManagerController(IApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index(int page = 1, int row = 10)
        {
            //  var Roles = _roleManager.Roles.Select(r => new RolesViewModel { RoleID = r.Id, RoleName = r.Name, Description = r.Description }).ToList();
            var Roles = _roleManager.GetAllRolesAndUserCount();
            var PagingModel = PagingList.Create(Roles, row, page);
            PagingModel.RouteValue = new RouteValueDictionary()
            {
                {"row",row }
            };

            return View(PagingModel);
        }

        [HttpGet]

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RolesViewModel rolesViewModel)
        {
            if (ModelState.IsValid)
            
            {

                var Result = await _roleManager.CreateAsync(new ApplicationRole(rolesViewModel.RoleName, rolesViewModel.Description));
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(rolesViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();

            }
            RolesViewModel rolesViewModel = new RolesViewModel()
            {
                RoleName = Role.Name,
                Description = Role.Description,
                RoleID = Role.Id,
                RecentRoleName=Role.Name
                
            };
            return View(rolesViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RolesViewModel rolesViewModel)
        {
            if (ModelState.IsValid)
            {
                var Role = await _roleManager.FindByIdAsync(rolesViewModel.RoleID);
                if (Role == null)
                {
                    return NotFound();
                }

                    Role.Name = rolesViewModel.RoleName;
                    Role.Description = rolesViewModel.Description;
                    var Result = await _roleManager.UpdateAsync(Role);
                    if (Result.Succeeded)
                    {
                        ViewBag.Success = "ذخیره تغییرات با موفقیت انجام شد";
                       
                    }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                
                
            }
            return View(rolesViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleltedRole(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            RolesViewModel rolesViewModel = new RolesViewModel()
            {
                RoleID = Role.Id,
                RoleName = Role.Name
            };
            return View(rolesViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteRole")]
       
        public async Task<IActionResult> DeletedRole(string id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(id);
            if(Role==null)
            {
                return NotFound();
            }
            var Result = await _roleManager.DeleteAsync(Role);
            if (Result.Succeeded)
            {
                return RedirectToAction("Index");

            }
            ViewBag.Error = "در حذف اطلاعات خطایی رخ داده است";
            RolesViewModel rolesViewModel = new RolesViewModel()
            {
                RoleID = Role.Id,
                RoleName = Role.Name
            };
            return View(rolesViewModel);
        }

    }
}

