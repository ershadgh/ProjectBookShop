using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectBookShop.Models.ViewModels;

namespace ProjectBookShop.Areas.Identity.Data
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        private readonly ApplicationIdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationUserManager> _logger;
        private readonly IOptions<IdentityOptions> _options;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IEnumerable<IPasswordValidator<ApplicationUser>> _passwordValidators;
        private readonly IServiceProvider _services;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IEnumerable<IUserValidator<ApplicationUser>> _userValidators;
        public ApplicationUserManager(ApplicationIdentityErrorDescriber errors, ILookupNormalizer keyNormalizer, ILogger<ApplicationUserManager> logger,
            IOptions<IdentityOptions> options,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            IServiceProvider services,
            IUserStore<ApplicationUser> userStore,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators) : base(userStore, options, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _errors = errors;
            _keyNormalizer = keyNormalizer;
            _logger = logger;
            _options = options;
            _passwordHasher = passwordHasher;
            _passwordValidators = passwordValidators;
            _services = services;
            _userStore = userStore;
            _userValidators = userValidators;
        }

        public async Task<List<ApplicationUser>> GetAllUserAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<List<UsersViewModel>> GetAllUsersWithRolesAsync()
        {
            return await Users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FrisName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                LastVisitDateTime = user.LastVisitDateTime,
                Image = user.Image,
                RegisterDate = user.RegisterDate,
                Roles = user.Roles.Select(p => p.Role.Name),
            }).ToListAsync();
        }


        public async Task<UsersViewModel> FindUserWithRolesByIdAsync(string UserID)
            {

            //var a= await Users.Where(user => user.Id == UserID).Select(user => new UsersViewModel
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    UserName = user.UserName,
            //    PhoneNumber = user.PhoneNumber,
            //    FirstName = user.FrisName,
            //    LastName = user.LastName,
            //    BirthDate = user.BirthDate,
            //    IsActive = user.IsActive,
            //    LastVisitDateTime = user.LastVisitDateTime,
            //    Image = user.Image,
            //    RegisterDate = user.RegisterDate,
            //    Roles = user.Roles.Select(p => p.Role.Name),
            //    AccessFailedCount = user.AccessFailedCount,
            //    EmailConfirmed = user.EmailConfirmed,
            //    LockoutEnabled = user.LockoutEnabled,
            //    LockoutEnd = user.LockoutEnd,
            //    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            //    TwoFactorEnabled = user.TwoFactorEnabled
            //}).FirstOrDefaultAsync();

            //return a;

            return await Users.Where(user => user.Id == UserID).Select(user => new UsersViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FrisName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                LastVisitDateTime = user.LastVisitDateTime,
                Image = user.Image,
                RegisterDate = user.RegisterDate,
                Roles = user.Roles.Select(p => p.Role.Name),
                AccessFailedCount = user.AccessFailedCount,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled
            }).FirstOrDefaultAsync();

        }
    public string NormalizeKey(string key)
    {
        throw new NotImplementedException();
    }
}
}