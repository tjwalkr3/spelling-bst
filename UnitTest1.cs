namespace spelling_bst;


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
}

public interface IVisitor<T> {
    void Visit(T value);
}

public class Accumulator<T> : IVisitor<T> {
    private List<T> accumulated;

    public Accumulator(List<T> whereToAccumulate) {
        accumulated = whereToAccumulate;
    }

    public void Visit(T value) {
        accumulated.Add(value);
    }
}

public class BinaryTreeNode<T> {
    private T value;
    public BinaryTreeNode<T>? left {get; set;}
    public BinaryTreeNode<T>? right {get; set;}

    public BinaryTreeNode(T value, BinaryTreeNode<T>? left = null, BinaryTreeNode<T>? right = null) {
        this.value = value;
        this.left = left;
        this.right = right;
    }

    public void TraverseInOrder(IVisitor<T> visitor) {
        if (left is not null) {
            left.TraverseInOrder(visitor);
        }
        visitor.Visit(value);
        if (right is not null) {
            right.TraverseInOrder(visitor);
        }
    }

    public void TraversePostOrder(IVisitor<T> visitor) {
        if (left is not null) {
            left.TraversePostOrder(visitor);
        }
        if (right is not null) {
            right.TraversePostOrder(visitor);
        }
        visitor.Visit(value);
    }

    public void TraversePreOrder(IVisitor<T> visitor) {
        visitor.Visit(value);
        if (left is not null) {
            left.TraversePreOrder(visitor);
        }
        if (right is not null) {
            right.TraversePreOrder(visitor);
        }
    }

    public int Size2() {
        Queue<BinaryTreeNode<T>> countQueue = new Queue<BinaryTreeNode<T>>();
        countQueue.Enqueue(this);
        int count = 1;

        while(countQueue.Count > 0) {
            BinaryTreeNode<T> currentNode = countQueue.Dequeue();

            // Check for left child, count and enqueue if found
            if (currentNode.left != null) {
                count++;
                countQueue.Enqueue(currentNode.left);
            }

            // Check for right child, count and enqueue if found
            if (currentNode.right != null) {
                count++;
                countQueue.Enqueue(currentNode.right);
            }
        }

        return count;
    }

    public int Height2() {
        Queue<BinaryTreeNode<T>> heightQueue = new Queue<BinaryTreeNode<T>>();
        heightQueue.Enqueue(this);
        int numOfNodes = heightQueue.Count;
        int height = 0;

        while(numOfNodes > 0) {
            // Unload the last layer of nodes and load the next
            while(numOfNodes > 0) {
                BinaryTreeNode<T> currentNode = heightQueue.Dequeue();
                if (currentNode.left != null) heightQueue.Enqueue(currentNode.left);
                if (currentNode.right != null) heightQueue.Enqueue(currentNode.right);
                numOfNodes--;
            }

            // Set the count for the current layer
            numOfNodes = heightQueue.Count;
            height++;
        }

        return height;
    }
}
