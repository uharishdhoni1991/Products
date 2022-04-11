using System;
using System.ComponentModel;
using System.Reflection;

namespace RandomTeamGenerator.Helpers
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum enumValue)
		{
            Type type = enumValue.GetType();
            MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumValue.ToString();
        }
	}
}
