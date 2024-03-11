using System.Collections.ObjectModel;
using System.Reflection;

namespace Core;

public class AssemblyLoader
{
    public static ObservableCollection<string> GetNamespaces(string assemblyPath)
    {
        var namespaces = new ObservableCollection<string>();
        var assembly = Assembly.LoadFrom(assemblyPath);
        var types = new List<Type>(assembly.GetTypes());

        var typeHierarchy = new TypeHierarchy(assembly.GetTypes());
        
        return null;
    }
    
}