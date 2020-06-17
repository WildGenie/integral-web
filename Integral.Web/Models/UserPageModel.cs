using System.Collections.Generic;
using System.Linq;
using Integral.Extensions;
using Integral.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Integral.Models
{
    public abstract class UserPageModel : ExtendedPageModel
    {
        protected UserPageModel(UserManager<ApplicationUser> userManager) => UserManager = userManager;

        protected UserManager<ApplicationUser> UserManager { get; }

        protected NotFoundObjectResult UserNotFoundResult() => UserNotFoundResult(UserManager.GetUserId(User));

        protected NotFoundObjectResult UserNotFoundResult(string id) => NotFound($"Unable to authenticate user '{ id }'.");

        protected PageResult IdentityErrorResult(IdentityResult identityResult) => ErrorResult(identityResult.Errors.Select(error => error.Description));

        protected PageResult ErrorResult(string error) => ErrorResult(new string[] { error });

        protected PageResult ErrorResult(IEnumerable<string> errors)
        {
            foreach (string error in errors)
            {
                ModelState.AddModelError(error);
            }

            return Page();
        }
    }
}
