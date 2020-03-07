using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zoomnews.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum genericEnum)
        {
            Type genericEnumType = genericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_attribs != null && _attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_attribs.ElementAt(0)).Description;
                }
            }
            return genericEnum.ToString();
        }

        public static List<T> GetItems<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            List<T> list = new List<T>();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                list.Add(value);
            }

            return list;
        }
    }
}
