using Newtonsoft.Json.Converters;
using System;

namespace Prorent24.Common.Extentions
{
    public static class TypeScriptExtention
    {
        public static string ConvertToTypeScriptType(this Type type, string includeType = "", string keyType = "")
        {
            if (!string.IsNullOrWhiteSpace(keyType))
            {
                return keyType;
            }
            else
            if (!string.IsNullOrWhiteSpace(includeType))
            {
                return "string";
            }
            else if (type == typeof(System.DateTime))
            {
                return "date";
            }
            else if (type == typeof(System.Nullable<System.DateTime>))
            {
                return "date";
            }
            else if (type == typeof(System.Nullable<bool>))
            {
                return "boolean";
            }
            else if (type.IsEnum)
            {
                return "enum";
            }
            else
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                        return "number";
                    case TypeCode.Boolean:
                        return "boolean";
                    case TypeCode.Char:
                        return "symbol";
                    case TypeCode.DateTime:
                        return "date";
                    default:
                        return "string";
                }
            }
        }
    }
}
