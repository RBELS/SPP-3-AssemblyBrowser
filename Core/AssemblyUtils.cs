using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class AssemblyUtils
    {
        public static List<string> GetInfoItems(Type type)
        {
            List<string> typeInfoList = new List<string>();

            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                typeInfoList.Add($"[Field] {fieldInfo.FieldType.FullName} {fieldInfo.Name}");
            }

            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                typeInfoList.Add($"[Property] {propertyInfo.PropertyType.FullName} {propertyInfo.Name}");
            }

            MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            foreach (MethodInfo methodInfo in methodInfos)
            {
                string parameters = string.Join(", ", methodInfo.GetParameters().Select(p => $"{p.ParameterType.FullName} {p.Name}"));
                typeInfoList.Add($"[Method] {methodInfo.ReturnType.FullName} {methodInfo.Name}({parameters})");
            }

            return typeInfoList;
        }
    }
}
