using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace Integral.Users
{
    public sealed class ApplicationUser : IdentityUser<int>
    {
        public Dictionary<string, string> GetPersonalData()
        {
            Dictionary<string, string> personalData = new Dictionary<string, string>();
            IEnumerable<PropertyInfo> properties = typeof(ApplicationUser).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (PropertyInfo propertyInfo in properties)
            {
                personalData.Add(propertyInfo.Name, propertyInfo.GetValue(this)?.ToString() ?? string.Empty);
            }

            return personalData;
        }
    }
}
