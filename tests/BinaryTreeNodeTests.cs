namespace tests;
using bst_code;

public class BinaryTreeNodeTests
{
    // Represents the expression 5 * 10 + 8 * 4 - 9
    private BinaryTreeNode<string> tree = new BinaryTreeNode<string>("-", 
        new BinaryTreeNode<string>("+", 
            new BinaryTreeNode<string>("*", 
                new BinaryTreeNode<string>("5", null, null), 
                new BinaryTreeNode<string>("10", null, null)), 
            new BinaryTreeNode<string>("*", 
                new BinaryTreeNode<string>("8", null, null), 
                new BinaryTreeNode<string>("4", null, null))), 
        new BinaryTreeNode<string>("9", null, null));

    [Test]
    public void TestInOrder()
    {
        List<string> inOrderAccumulated = new List<string>();
        tree.TraverseInOrder(new Accumulator<string>(inOrderAccumulated));
        Assert.That(inOrderAccumulated, Is.EqualTo(new List<string?>(){"5", "*", "10", "+", "8", "*", "4", "-", "9"}));
    }

    [Test]
    public void TestPostOrder()
    {
        List<string> postOrderAccumulated = new List<string>();
        tree.TraversePostOrder(new Accumulator<string>(postOrderAccumulated));
        Assert.That(postOrderAccumulated, Is.EqualTo(new List<string?>(){"5", "10", "*", "8", "4", "*", "+", "9", "-"}));
    }

    [Test]
    public void TestPreOrder()
    {
        List<string> preOrderAccumulated = new List<string>();
        tree.TraversePreOrder(new Accumulator<string>(preOrderAccumulated));
        Assert.That(preOrderAccumulated, Is.EqualTo(new List<string>(){"-", "+", "*", "5", "10", "*", "8", "4", "9"}));
    }

    [Test]
    public void TestSize2()
    {
        Assert.That(tree.Size(), Is.EqualTo(9));
        Assert.That(tree.left?.Size(), Is.EqualTo(7));
        Assert.That(tree.left?.left?.Size(), Is.EqualTo(3));
        Assert.That(tree.left?.left?.left?.Size(), Is.EqualTo(1));
    }

    [Test]
    public void TestHeight2()
    {
        Assert.That(tree.Height(), Is.EqualTo(4));
        Assert.That(tree.left?.Height(), Is.EqualTo(3));
        Assert.That(tree.left?.left?.Height(), Is.EqualTo(2));
        Assert.That(tree.left?.left?.left?.Height(), Is.EqualTo(1));
    }
}
