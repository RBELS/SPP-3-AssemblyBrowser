using Core;

namespace Tests;

public class Tests
{
    [Test]
    public void GetTypeHierarchyTest()
    {
        var hierarchy = AssemblyLoader.GetTypeHierarchy(@"/Users/artyom/Documents/study/poit/6sem/spp/Assembly Browser/Faker.dll");
        var source = hierarchy.GetSource();
        
        Assert.Multiple(() =>
        {
            Assert.That(source.Target, Is.Empty);
            Assert.That(source.Types, Is.Empty);
            Assert.That(source.Children, Has.Count.EqualTo(4));
        });
        
        var exampleNode = source.Children.Find(node => "Example".Equals(node.Target));
        
        Assert.Multiple(() =>
        {
            Assert.That(exampleNode.Children, Is.Empty);
            Assert.That(exampleNode.Types, Has.Count.EqualTo(5));
        });
        foreach (var exampleNodeType in exampleNode.Types)
        {
            Assert.That(exampleNodeType.Namespace, Is.EqualTo("Example"));
        }
    }
}