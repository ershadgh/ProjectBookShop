using Microsoft.AspNetCore.Identity;

namespace ProjectBookShop.Areas.Identity.Data
{
    public class ApplicationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = nameof(DuplicateUserName), Description = $"نام کاربری '{userName}' تکراری وارد شده است" };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "کلمه عبور حداقل شامل یک کاراکتر غیر حرفی و غیر عددی باشد(@,%,#...)" };

        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "کلمه عبور باید حداقل شامل یک عدد باشد(0-9)" };

        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = nameof(PasswordRequiresLower), Description = ("کلمه عبور باید حداقل شامل یک حرف کوچک باشد(a-z)") };

        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = ("کلمه عبور حدافل باید شامل یک حرف بزرگ باشد(A-Z)") };

        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = nameof(PasswordTooShort), Description = $"کلمه عبور باید حداقل شامل {length} باشد" };

        public override IdentityError InvalidUserName(string? userName) => new IdentityError { Code = nameof(InvalidUserName), Description = "نام کاربری باید شامل کاراکترهای (a-z) و(0-9) باشد" };

        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = nameof(DuplicateEmail), Description = $"شما با ایمیل '{email}' قبلا ثبت نام کرده اید. " };
        public override IdentityError DuplicateRoleName(string role) => new IdentityError { Code = nameof(DuplicateRoleName), Description = $"تکراری است'{role}'" };
    



    }
}
