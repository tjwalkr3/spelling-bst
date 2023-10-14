namespace bst_code;

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
