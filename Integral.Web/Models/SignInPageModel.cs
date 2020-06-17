using Integral.Users;
using Microsoft.AspNetCore.Identity;

namespace Integral.Models
{
    public abstract class SignInPageModel : UserPageModel
    {
        protected SignInPageModel(SignInManager<ApplicationUser> signInManager) : base(signInManager.UserManager) => SignInManager = signInManager;

        protected SignInManager<ApplicationUser> SignInManager { get; }
    }
}
