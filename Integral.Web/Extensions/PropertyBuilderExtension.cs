using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integral.Extensions
{
    public static class PropertyBuilderExtension
    {
        public static PropertyBuilder<bool> HasBooleanIntegerConverstion(this PropertyBuilder<bool> propertyBuilder)
        {
            return propertyBuilder.HasConversion(boolean => boolean ? 1 : 0, integer => integer == 1);
        }
    }
}
