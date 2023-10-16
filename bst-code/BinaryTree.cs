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

    private BinaryTreeNode<T> AddRecursive(BinaryTreeNode<T>? currentNode, BinaryTreeNode<T> newNode)
    {
        // If the current node is null, set currentNode to newNode.
        if (currentNode == null) {
            currentNode = newNode;
            return currentNode;
        }

        // If the key of the new node is less than the current node's key, go left.
        else if (newNode.GetValue().CompareTo(currentNode.GetValue()) < 0) {
            currentNode.left = AddRecursive(currentNode.left, newNode);
            currentNode = balance(currentNode);
        }

        // If the key of the new node is greater than the current node's key, go right.
        else if (newNode.GetValue().CompareTo(currentNode.GetValue()) > 0) {
            currentNode.right = AddRecursive(currentNode.right, newNode);
            currentNode = balance(currentNode);
        }

        return currentNode;
    }


    private BinaryTreeNode<T>? RemoveRecursive(BinaryTreeNode<T>? currentNode, T key)
    {
        // Key not found, no action needed.
        if (currentNode == null) {
            return currentNode;
        }

        // If the key is less than the current node's key, go left. 
        else if (key.CompareTo(currentNode.GetValue()) < 0) {
            currentNode.left = RemoveRecursive(currentNode.left, key);
            currentNode = balance(currentNode);
        }

        // If the key is less than the current node's key, go right. 
        else if (key.CompareTo(currentNode.GetValue()) > 0) {
            currentNode.right = RemoveRecursive(currentNode.right, key);
            currentNode = balance(currentNode);
        }
        
        // You found the node to delete, now consider different cases
        else {
            // A child is null
            if (currentNode.right == null || currentNode.left == null) {
                BinaryTreeNode<T>? temp;
                if (currentNode.left == null) {
                    temp = currentNode.right;
                } else {
                    temp = currentNode.left;
                }
                
                // Both Left and Right Children are null
                if (temp == null) {
                    currentNode = null;

                // One of the children is null
                } else {
                    currentNode = temp;
                }

            // Neither of the children are null
            } else {
                BinaryTreeNode<T>? temp = currentNode.right; 
                while (temp.left != null) {
                    temp = temp.left;
                }
                currentNode.SetValue(temp.GetValue());
                currentNode.right = RemoveRecursive(currentNode.right, temp.GetValue());
            }

            if (currentNode != null) {
                currentNode = balance(currentNode);
            }
        }

        return currentNode;
    }

    private BinaryTreeNode<T> balance(BinaryTreeNode<T> current) {
        int diff = leftRightDifference(current);
        
        if (diff > 1) {
            if (leftRightDifference(current.left) > 0) {
                current = RotateLeftLeft(current);
            } else {
                current = RotateLeftRight(current);
            }
        } else if (diff < -1) {
            if (leftRightDifference(current.right) > 0) {
                current = RotateRightLeft(current);
            } else {
                current = RotateRightRight(current);
            }
        }
        
        return current;
    }

    // Get the difference in height between the left and right branches
    int leftRightDifference(BinaryTreeNode<T>? node) 
    {
        int leftHeight = 0;
        int rightHeight = 0;

        if (node == null) return 0;
        if (node.left != null) leftHeight = node.left.Height();
        if (node.right != null) rightHeight = node.right.Height();
        
        return leftHeight - rightHeight; 
    }

    private BinaryTreeNode<T> RotateRightRight(BinaryTreeNode<T> parent)
    {
        BinaryTreeNode<T> newParent = parent.right!;
        parent.right = newParent.left;
        newParent.left = parent;
        return newParent;
    }

    private BinaryTreeNode<T> RotateRightLeft(BinaryTreeNode<T> parent)
    {
        BinaryTreeNode<T> newParent = parent.right!;
        parent.right = RotateLeftLeft(newParent);
        return RotateRightRight(parent);
    }

    private BinaryTreeNode<T> RotateLeftLeft(BinaryTreeNode<T> parent)
    {
        BinaryTreeNode<T> newParent = parent.left!;
        parent.left = newParent.right;
        newParent.right = parent;
        return newParent;
    }

    private BinaryTreeNode<T> RotateLeftRight(BinaryTreeNode<T> parent)
    {
        BinaryTreeNode<T> pivot = parent.left!;
        parent.left = RotateRightRight(pivot);
        return RotateLeftLeft(parent);
    }

    public List<T> TraverseInOrder() {
        List<T> accumulated = new List<T>();
        Accumulator<T> visitor = new Accumulator<T>(accumulated);
        if (root != null) root.TraverseInOrder(visitor);

        return accumulated;
    }

    public List<T> TraversePostOrder() {
        List<T> accumulated = new List<T>();
        Accumulator<T> visitor = new Accumulator<T>(accumulated);
        if (root != null) root.TraversePostOrder(visitor);

        return accumulated;
    }

    public List<T> TraversePreOrder() {
        List<T> accumulated = new List<T>();
        Accumulator<T> visitor = new Accumulator<T>(accumulated);
        if (root != null) root.TraversePreOrder(visitor);

        return accumulated;
    }
}
