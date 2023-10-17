namespace tests;
using bst_code;

public class BinaryTreeTests {

    // This test verifies that Add(), InOrder(), and the balancing functionality are all functional
    [Test]
    public void TestAdding()
    {
        Tree<int> tree = new Tree<int>();
        Assert.That(tree.Add(1), Is.EqualTo(true));
        Assert.That(tree.Add(2), Is.EqualTo(true));
        Assert.That(tree.Add(3), Is.EqualTo(true));
        Assert.That(tree.Add(4), Is.EqualTo(true));
        Assert.That(tree.Add(5), Is.EqualTo(true));
        Assert.That(tree.Add(5), Is.EqualTo(false)); // Test to reject repeats
        Assert.That(tree.Add(2), Is.EqualTo(false)); // Test to reject repeats
        Assert.That(tree.Add(6), Is.EqualTo(true));
        Assert.That(tree.Add(7), Is.EqualTo(true));

        // Get the items from the iterator, store them in a list, and check them against the actual preorder
        List<int> inOrderAccumulated = new List<int>();
        foreach (int item in tree.InOrder()) inOrderAccumulated.Add(item);
        Assert.That(inOrderAccumulated, Is.EqualTo(new List<int>(){1, 2, 3, 4, 5, 6, 7}));
    }


    // This test verifies that the Remove() function will throw exceptions if a key is not found
    [Test]
    public void TestRemove()
    {
        Tree<int> tree = new();

        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        tree.Add(4);

        Assert.That(tree.Remove(3), Is.EqualTo(3));
        Assert.Throws<KeyNotFoundException>(() => {tree.Remove(3);});
        Assert.Throws<KeyNotFoundException>(() => {tree.Remove(5);});
    }


    // This test verifies that the Find() function will throw exceptions if a key is not found
    [Test]
    public void TestFind()
    {
        Tree<int> tree = new();

        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        tree.Add(4);

        Assert.That(tree.Find(3), Is.EqualTo(3));
        Assert.Throws<KeyNotFoundException>(() => {tree.Find(5);});
    }


    [Test]
    public void TestSize()
    {
        Tree<int> tree = new();

        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        tree.Add(4);

        Assert.That(tree.Size(), Is.EqualTo(4));
    }
}