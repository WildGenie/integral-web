using System.Text;
using System.Threading.Tasks;
using Integral.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Integral.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<string> GenerateEncodedEmailConfirmationTokenAsync(this UserManager<ApplicationUser> userManager, ApplicationUser applicationUser)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public static async Task<string> GenerateEncodedPasswordResetTokenAsync(this UserManager<ApplicationUser> userManager, ApplicationUser applicationUser)
        {
            string token = await userManager.GeneratePasswordResetTokenAsync(applicationUser);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }
    }
}
