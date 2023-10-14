namespace tests;
using bst_code;

public class Tests
{
    // Represents the expression 5 * 10 + 8 * 4 - 9
    private BinaryTreeNode<string?> tree = new BinaryTreeNode<string?>("-", 
        new BinaryTreeNode<string?>("+", 
            new BinaryTreeNode<string?>("*", 
                new BinaryTreeNode<string?>("5", null, null), 
                new BinaryTreeNode<string?>("10", null, null)), 
            new BinaryTreeNode<string?>("*", 
                new BinaryTreeNode<string?>("8", null, null), 
                new BinaryTreeNode<string?>("4", null, null))), 
        new BinaryTreeNode<string?>("9", null, null));

    [Test]
    public void TestInOrder()
    {
        List<string?> inOrderAccumulated = new List<string?>();
        tree.TraverseInOrder(new Accumulator<string?>(inOrderAccumulated));
        Assert.That(inOrderAccumulated, Is.EqualTo(new List<string?>(){"5", "*", "10", "+", "8", "*", "4", "-", "9"}));
    }

    [Test]
    public void TestPostOrder()
    {
        List<string?> postOrderAccumulated = new List<string?>();
        tree.TraversePostOrder(new Accumulator<string?>(postOrderAccumulated));
        Assert.That(postOrderAccumulated, Is.EqualTo(new List<string?>(){"5", "10", "*", "8", "4", "*", "+", "9", "-"}));
    }

    [Test]
    public void TestPreOrder()
    {
        List<string?> preOrderAccumulated = new List<string?>();
        tree.TraversePreOrder(new Accumulator<string?>(preOrderAccumulated));
        Assert.That(preOrderAccumulated, Is.EqualTo(new List<string?>(){"-", "+", "*", "5", "10", "*", "8", "4", "9"}));
    }

    [Test]
    public void TestSize2()
    {
        Assert.That(tree.Size2(), Is.EqualTo(9));
        Assert.That(tree.left?.Size2(), Is.EqualTo(7));
        Assert.That(tree.left?.left?.Size2(), Is.EqualTo(3));
        Assert.That(tree.left?.left?.left?.Size2(), Is.EqualTo(1));
    }

    [Test]
    public void TestHeight2()
    {
        Assert.That(tree.Height2(), Is.EqualTo(4));
        Assert.That(tree.left?.Height2(), Is.EqualTo(3));
        Assert.That(tree.left?.left?.Height2(), Is.EqualTo(2));
        Assert.That(tree.left?.left?.left?.Height2(), Is.EqualTo(1));
    }

    //add tests
    
    [Test]
    public void TestAddOverwrite()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(5);
        BinaryTreeNode<int> node = new BinaryTreeNode<int>(5);

        root.Add(node, 5);

        Assert.That(root.GetValue(), Is.EqualTo(5));
    }

    [Test]
    public void TestAddToLeft()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(5);
        BinaryTreeNode<int> node = new BinaryTreeNode<int>(3);

        root.Add(node, 3);

        Assert.That(root.left, Is.EqualTo(node));
    }

    [Test]
    public void TestAddToRight()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(5);
        BinaryTreeNode<int> node = new BinaryTreeNode<int>(8);

        root.Add(node, 8);

        Assert.That(root.right, Is.EqualTo(node));
    }

    //Remove tests
    [Test]
    public void TestAddNode()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(10);
        BinaryTreeNode<int> node = new BinaryTreeNode<int>(5);

        root.Add(node, 5);

        Assert.That(root.left, Is.EqualTo(node));
    }

    [Test]
    public void TestRemoveLeafNode()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(10);
        BinaryTreeNode<int> node = new BinaryTreeNode<int>(5);

        root.Add(node, 5);
        root.Remove(root, 5);

        Assert.That(root.left, Is.Null);
    }

    [Test]
    public void TestRemoveNodeWithOneChild()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(10);
        BinaryTreeNode<int> leftNode = new BinaryTreeNode<int>(5);

        root.Add(leftNode, 5);
        root.Remove(root, 5);

        Assert.That(root.left, Is.Null);
    }

    [Test]
    public void TestRemoveNodeWithTwoChildren()
    {
        BinaryTreeNode<int> root = new BinaryTreeNode<int>(10);
        BinaryTreeNode<int> leftNode = new BinaryTreeNode<int>(5);
        BinaryTreeNode<int> rightNode = new BinaryTreeNode<int>(15);

        root.Add(leftNode, 5);
        root.Add(rightNode, 15);
        root.Remove(root, 10);

        Assert.That(root.GetValue(), Is.EqualTo(15));
        Assert.That(root.left, Is.EqualTo(leftNode));
        Assert.That(root.right, Is.Null);
    }
}