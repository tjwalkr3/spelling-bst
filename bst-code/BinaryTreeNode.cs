namespace bst_code;

public class BinaryTreeNode<T> where T: IComparable<T> {
    private T value;
    public int height;
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

    public T SetValue(T newValue) {
        T temp = value;
        value = newValue;
        return temp;
    }

    public int Size() {
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

    public int Height() {
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
