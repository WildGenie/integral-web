using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Integral.Extensions
{
    public static class ModelStateDictionaryExtension
    {
        public static void AddModelError(this ModelStateDictionary modelStateDictionary, string error) => modelStateDictionary.AddModelError(string.Empty, error);
    }
}
