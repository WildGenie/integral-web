using System.ComponentModel.DataAnnotations;

namespace Integral.Attributes
{
    public sealed class PasswordLengthAttribute : StringLengthAttribute
    {
        public PasswordLengthAttribute() : base(100)
        {
            ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.";
            MinimumLength = 6;
        }
    }
}
