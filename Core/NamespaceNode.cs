namespace Core;

public class NamespaceNode
{
    public string Target { get; set; } = "";
    public List<NamespaceNode> Children { get; set; } = new List<NamespaceNode>();
    public List<Type> Types { get; set; } = new List<Type>();
}