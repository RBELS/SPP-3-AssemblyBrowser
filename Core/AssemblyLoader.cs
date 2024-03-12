using System.Reflection;

namespace Core;

public static class AssemblyLoader
{
    public static TypeHierarchy GetTypeHierarchy(string assemblyPath)
    {
        return new TypeHierarchy(Assembly.LoadFrom(assemblyPath).GetTypes());
    }
}