using System.ComponentModel;
using System.Reflection;

namespace FinanceAPI.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type genericEnumType = value.GetType();
            MemberInfo[] memberInfos = genericEnumType.GetMember(value.ToString());
            return InitializeDescription(value, memberInfos);
        }

        private static string InitializeDescription(Enum value, MemberInfo[] memberInfos)
        {
            var attributes = memberInfos[0].GetCustomAttributes(
                attributeType: typeof(DescriptionAttribute),
                inherit: false);
            return attributes?.Any() ?? false
                ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                : value.ToString();
        }
    }
}
