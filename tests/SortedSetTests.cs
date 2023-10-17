namespace tests;
using bst_code;
using sortedset;

public class SortedSetTests {
    [Test]
    public void TestAdd()
    {
        BSTSortedSet<int> sortedSet = new();

        Assert.That(sortedSet.Add(1), Is.EqualTo(true));
        Assert.That(sortedSet.Add(2), Is.EqualTo(true));
        Assert.That(sortedSet.Add(3), Is.EqualTo(true));
        Assert.That(sortedSet.Add(4), Is.EqualTo(true));
        Assert.That(sortedSet.Add(5), Is.EqualTo(true));
        Assert.That(sortedSet.Add(5), Is.EqualTo(false));
        Assert.That(sortedSet.Add(2), Is.EqualTo(false));
    }

    [Test]
    public void TestRemove()
    {
        BSTSortedSet<int> sortedSet = new();

        sortedSet.Add(1);
        sortedSet.Add(2);
        sortedSet.Add(3);
        sortedSet.Add(4);

        Assert.That(sortedSet.Remove(3), Is.EqualTo(3));
        Assert.Throws<KeyNotFoundException>(() => {sortedSet.Remove(3);});
        Assert.Throws<KeyNotFoundException>(() => {sortedSet.Remove(5);});
    }

    [Test]
    public void TestFind()
    {
        BSTSortedSet<int> sortedSet = new();

        sortedSet.Add(1);
        sortedSet.Add(2);
        sortedSet.Add(3);
        sortedSet.Add(4);

        Assert.That(sortedSet.Find(3), Is.EqualTo(3));
        Assert.Throws<KeyNotFoundException>(() => {sortedSet.Find(5);});
    }

    [Test]
    public void TestSize()
    {
        BSTSortedSet<int> sortedSet = new();

        sortedSet.Add(1);
        sortedSet.Add(2);
        sortedSet.Add(3);
        sortedSet.Add(4);

        Assert.That(sortedSet.Size(), Is.EqualTo(4));
    }
}