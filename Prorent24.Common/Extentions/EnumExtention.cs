using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Prorent24.Common.Extentions
{
    public static class EnumExtention
    {
        public static int? GetDisplayOrder<Enum>(this Enum enumValue)
        {
            return enumValue.GetType()?
                            .GetMember(enumValue.ToString())?
                            .First()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Order;
        }

        public static List<KeyValuePair<string, int>> GetEnumList<Enum>()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var e in System.Enum.GetValues(typeof(Enum)))
            {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }
            return list;
        }

        public static List<KeyValuePair<string, int>> ToKayValue(this Type enumType)
        {
            if (enumType.IsEnum)
            {
                var list = new List<KeyValuePair<string, int>>();
                foreach (var e in System.Enum.GetValues(enumType))
                {
                    list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
                }
                return list;
            }
            return null;
        }

        public static Expected GetAttributeValue<T, Expected>(this System.Enum enumeration, Func<T, Expected> expression)
        where T : Attribute
        {
            T attribute =
              enumeration
                .GetType()
                .GetMember(enumeration.ToString())
                .Where(member => member.MemberType == MemberTypes.Field)
                .FirstOrDefault()
                .GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .SingleOrDefault();

            if (attribute == null)
                return default(Expected);

            return expression(attribute);
        }
    }
}
