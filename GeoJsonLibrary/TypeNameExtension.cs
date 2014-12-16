using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJsonLibrary
{
    //Code obtained from: http://stackoverflow.com/questions/6402864/c-pretty-type-name-function/21269486#21269486
    public static class TypeNameExtension
    {
        /// <summary>
        /// Get full type name with full namespace names
        /// </summary>
        /// <param name="type">
        /// The type to get the C# name for (may be a generic type or a nullable type).
        /// </param>
        /// <returns>
        /// Full type name, fully qualified namespaces
        /// </returns>
        public static string CSharpName(this Type type)
        {
            Type nullableType = Nullable.GetUnderlyingType(type);
            string nullableText;
            if (nullableType != null)
            {
                type = nullableType;
                nullableText = "?";
            }
            else
            {
                nullableText = string.Empty;
            }

            if (type.IsGenericType)
            {
                return string.Format(
                    "{0}<{1}>{2}",
                    type.Name.Substring(0, type.Name.IndexOf('`')),
                    string.Join(", ", type.GetGenericArguments().Select(ga => ga.CSharpName())),
                    nullableText);
            }

            switch (type.Name)
            {
                case "String":
                    return "string";
                case "Int32":
                    return "int" + nullableText;
                case "Decimal":
                    return "decimal" + nullableText;
                case "Object":
                    return "object" + nullableText;
                case "Void":
                    return "void" + nullableText;
                default:
                    return (string.IsNullOrWhiteSpace(type.FullName) ? type.Name : type.FullName) + nullableText;
            }
        }
    }
}
