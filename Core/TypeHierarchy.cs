namespace Core;

public class TypeHierarchy
{
    private NamespaceNode _source;

    public NamespaceNode GetSource()
    {
        return _source;
    }
    
    public TypeHierarchy(Type[] types)
    {
        var groupTypes = types.GroupBy(type => GetDepth(type.Namespace)).ToList();
        _source = new NamespaceNode();
        _source.Target = "";

        var list = new List<NamespaceNode>() { _source };
        
        foreach (var type in types)
        {
            Insert(list, type.Namespace, type); // Сюда передавать типы и вкладывать в дерево в нижний уровень как-то
        }
    }

    private void Insert(List<NamespaceNode> nsNodes, string ns, Type type)
    {
        foreach (var namespaceNode in nsNodes)
        {
            var result = false;
            if (ns.StartsWith(namespaceNode.Target))
            {
                if (namespaceNode.Target.Length + 1 <= ns.Length)
                {
                    var newLength = ns[namespaceNode.Target.Length] == '.' ? namespaceNode.Target.Length+1 : namespaceNode.Target.Length;
                    Insert(namespaceNode.Children, ns.Substring(newLength), type);
                }
                else
                {
                    namespaceNode.Types.Add(type);
                }
                return;
            }
        }
        var newNsNode = new NamespaceNode();
        newNsNode.Target = ns.Split(".")[0];
        nsNodes.Add(newNsNode);
        if (newNsNode.Target.Length + 1 <= ns.Length)
        {
            Insert(newNsNode.Children, ns.Substring(newNsNode.Target.Length+1), type);
        }
        else
        {
            newNsNode.Types.Add(type);
        }
    }
    
    private int GetDepth(string ns)
    {
        var arr = ns.Split(".");
        return arr.Length;
    }
}