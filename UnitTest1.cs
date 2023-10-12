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

public class BInaryTree<T> where T: IComparable<T> {
    BinaryTreeNode<T>? root = null;

//     private ref BinaryTreeNode<T>? FindNode(T value) {
//         if (root == null || root.GetValue().CompareTo(value) == 0) {
//             return ref root;
//         } else if (value.CompareTo(root.GetValue()) < 0) {
            
//         }
//     }
}

public class BinaryTreeNode<T> where T: IComparable<T> {
    private T value;
    public BinaryTreeNode<T>? left {get; set;}
    public BinaryTreeNode<T>? right {get; set;}

    public BinaryTreeNode(T value, BinaryTreeNode<T>? left = null, BinaryTreeNode<T>? right = null) {
        this.value = value;
        this.left = left;
        this.right = right;
    }


    public T GetValue() {
        return value;
    }

    public void Add(BinaryTreeNode<T> newNode, T key)
    {
        // If the current node has the same key as the new node, update its value.
        if (key.Equals(this.GetValue()))
        {
            this.value = newNode.value;
        }
        // If the key of the new node is less than the current node's key, go left.
        else if (key.CompareTo(this.GetValue()) < 0)
        {
            if (left == null)
            {
                left = newNode;
            }
            else
            {
                left.Add(newNode, key);
            }
        }
        // If the key of the new node is greater than the current node's key, go right.
        else if (key.CompareTo(this.GetValue()) > 0)
        {
            if (right == null)
            {
                right = newNode;
            }
            else
            {
                right.Add(newNode, key);
            }
        }
    }


    public BinaryTreeNode<T> Remove(BinaryTreeNode<T> node, T key)
    {
        if (node == null)
        {
            return node; // Key not found, no action needed.
        }

        // Recursively search for the key to be removed.
        int comparison = key.CompareTo(node.GetValue());
        if (comparison < 0)
        {
            node.left = Remove(node.left, key);
        }
        else if (comparison > 0)
        {
            node.right = Remove(node.right, key);
        }
        else
        {
            // Key found, consider the different cases.
            if (node.left == null)
            {
                return node.right; // Case 1: Node with one child or no child.
            }
            else if (node.right == null)
            {
                return node.left; // Case 1: Node with one child.
            }

            // Case 2: Node with two children.
            // Find the in-order successor (smallest in the right subtree).
            node.value = MinValue(node.right);

            // Remove the in-order successor.
            node.right = Remove(node.right, node.GetValue());
        }

        return node;
    }

    private T MinValue(BinaryTreeNode<T> node)
    {
        T minValue = node.GetValue();
        while (node.left != null)
        {
            minValue = node.left.GetValue();
            node = node.left;
        }
        return minValue;
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
