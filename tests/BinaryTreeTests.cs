namespace tests;
using bst_code;

public class BinaryTreeTests {
    //add tests
    [Test]
    public void TestAdding()
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        tree.Add(4);
        tree.Add(2);
        tree.Add(6);
        tree.Add(1);
        tree.Add(3);
        tree.Add(5);
        tree.Add(7);

        List<int> inOrderAccumulated = tree.TraverseInOrder();
        //Console.WriteLine($"Adding Test: {string.Join(',', inOrderAccumulated.ToArray())}");
        Assert.That(inOrderAccumulated, Is.EqualTo(new List<int>(){1, 2, 3, 4, 5, 6, 7}));
    }

    [Test]
    public void TestRemoving()
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        tree.Add(4);
        tree.Add(2);
        tree.Add(6);
        tree.Add(1);
        tree.Add(3);
        tree.Add(5);
        tree.Add(7);

        tree.Remove(7); // no children
        tree.Remove(6); // one child
        tree.Remove(2); // two children

        List<int> inOrderAccumulated = tree.TraverseInOrder();
        //Console.WriteLine($"Removing Test: {string.Join(',', inOrderAccumulated.ToArray())}");
        Assert.That(inOrderAccumulated, Is.EqualTo(new List<int>(){1, 3, 4, 5}));
    }

    [Test]
    public void TestBalancing()
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        tree.Add(4);
        tree.Add(5);
        tree.Add(7);
        tree.Add(6);

        List<int> inOrderAccumulated = tree.TraverseInOrder();
        //Console.WriteLine($"Balancing Test: {string.Join(',', inOrderAccumulated.ToArray())}");
        Assert.That(inOrderAccumulated, Is.EqualTo(new List<int>(){1, 2, 3, 4, 5, 6, 7}));
    }

    [Test]
    public void TestFind()
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        tree.Add(4);

        Assert.That(tree.Find(1), Is.EqualTo(true));
        Assert.That(tree.Find(2), Is.EqualTo(true));
        Assert.That(tree.Find(3), Is.EqualTo(true));
        Assert.That(tree.Find(4), Is.EqualTo(true));
        Assert.That(tree.Find(5), Is.EqualTo(false));
    }
}