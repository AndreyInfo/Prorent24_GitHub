using System.Linq;
using System.Reflection;

namespace Prorent24.Common.Extentions
{
    public static class MergeExtention
    {
        public static To MergeEntity<From, To>(this From from,To to)
        {
            PropertyInfo[] fromProperties = from.GetType().GetProperties();
            PropertyInfo[] toProperties = from.GetType().GetProperties();

            foreach (PropertyInfo toProperty in toProperties)
            {
                PropertyInfo fromProperty = fromProperties.FirstOrDefault(x => x.Name == toProperty.Name);

                object toValue = toProperty.GetValue(to);
                object fromValue = fromProperty.GetValue(from);

                object value = fromValue != null && fromValue != toValue ? fromValue : toValue;

                toProperty.SetValue(to, value);
            }

            return to;

        }
    }
}
