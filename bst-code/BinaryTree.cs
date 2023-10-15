namespace bst_code;

public class BinaryTree<T> where T: IComparable<T> {
    BinaryTreeNode<T>? root = null;

    public void Add(T data) {
        BinaryTreeNode<T> newItem = new BinaryTreeNode<T>(data);
        if (root == null) {
            root = newItem;
        } else {
            root = AddRecursive(root, newItem);
        }
    }

    public void Remove(T key) {
        root = RemoveRecursive(root, key);
    }

    private static BinaryTreeNode<T> AddRecursive(BinaryTreeNode<T>? currentNode, BinaryTreeNode<T> newNode)
    {
        // If the current node is null, set currentNode to newNode.
        if (currentNode == null) {
            currentNode = newNode;
            return currentNode;
        }

        // If the key of the new node is less than the current node's key, go left.
        else if (newNode.GetValue().CompareTo(currentNode.GetValue()) < 0) {
            currentNode.left = AddRecursive(currentNode.left, newNode);
            //balance(currentNode);
        }

        // If the key of the new node is greater than the current node's key, go right.
        else if (newNode.GetValue().CompareTo(currentNode.GetValue()) > 0) {
            currentNode.right = AddRecursive(currentNode.right, newNode);
            //balance(currentNode);
        }

        return currentNode;
    }


    private static BinaryTreeNode<T>? RemoveRecursive(BinaryTreeNode<T>? currentNode, T key)
    {
        // Key not found, no action needed.
        if (currentNode == null) {
            return currentNode;
        }

        // Recursively compare the key to the data in the current node and find the node to delete
        int comparison = key.CompareTo(currentNode.GetValue());
        if (comparison < 0) 
            currentNode.left = RemoveRecursive(currentNode.left, key);
        else if (comparison > 0)
            currentNode.right = RemoveRecursive(currentNode.right, key);
        
        // You found the node to delete, now consider different cases
        else {

            if (currentNode.right == null || currentNode.left == null) {
                BinaryTreeNode<T>? temp;
                if (currentNode.left == null) {
                    temp = currentNode.right;
                } else {
                    temp = currentNode.left;
                }

                if (temp == null) {
                    currentNode = null;
                } else {
                    currentNode = temp;
                }
            } else {
                BinaryTreeNode<T>? temp = currentNode.right;
                while (temp.left != null) {
                    temp = temp.left;
                }
                currentNode.SetValue(temp.GetValue());
                currentNode.right = RemoveRecursive(currentNode.right, temp.GetValue());
            }

            //balance(currentNode);
            return currentNode;
        }

        return currentNode;
    }

    public List<T> TraverseInOrder() {
        List<T> accumulated = new List<T>();
        Accumulator<T> visitor = new Accumulator<T>(accumulated);
        if (root != null) root.TraverseInOrder(visitor);

        return accumulated;
    }
}
