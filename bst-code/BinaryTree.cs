namespace bst_code;

public class Tree<T> : ISortedSet<T>, ITraversable<T> where T : IComparable<T> {
    BinaryTreeNode<T>? root = null;

    public T Find(T data) {
        if (FindRecursive(root, data)) {
            return data;
        } else {
            throw new KeyNotFoundException("Item could not be found in this SortedSet");
        }
    }

    private bool FindRecursive(BinaryTreeNode<T>? currentNode, T key) {
        // Key not found, return false.
        if (currentNode == null) {
            return false;
        }

        // If the key is less than the current node's key, go left. 
        else if (key.CompareTo(currentNode.GetValue()) < 0) {
            return FindRecursive(currentNode.left, key);
        }

        // If the key is less than the current node's key, go right. 
        else if (key.CompareTo(currentNode.GetValue()) > 0) {
            return FindRecursive(currentNode.right, key);
        }
        
        // You found the node
        else {
            return true;
        }
    }

    public bool Add(T data) {
        if (root == null) {
            root = new BinaryTreeNode<T>(data);
            return true;
        } else {
            int originalSize = root.Size();

            root = AddRecursive(root, new BinaryTreeNode<T>(data));

            if (root!.Size() > originalSize) {
                return true;
            } else {
                return false;
            }
        }
    }

    private BinaryTreeNode<T>? AddRecursive(BinaryTreeNode<T>? currentNode, BinaryTreeNode<T> newNode) {
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

    public T Remove(T key) {
        bool failed = false;
        root = RemoveRecursive(root, key, ref failed);

        if (failed) {
            throw new KeyNotFoundException("Item could not be found in this SortedSet");
        } else {
            return key;
        }
    }

    private BinaryTreeNode<T>? RemoveRecursive(BinaryTreeNode<T>? currentNode, T key, ref bool failed) {
        // Key not found, no action needed.
        if (currentNode == null) {
            failed = true;
            return currentNode;
        }

        // If the key is less than the current node's key, go left. 
        else if (key.CompareTo(currentNode.GetValue()) < 0) {
            currentNode.left = RemoveRecursive(currentNode.left, key, ref failed);
            currentNode = balance(currentNode);
        }

        // If the key is less than the current node's key, go right. 
        else if (key.CompareTo(currentNode.GetValue()) > 0) {
            currentNode.right = RemoveRecursive(currentNode.right, key, ref failed);
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
                currentNode.right = RemoveRecursive(currentNode.right, temp.GetValue(), ref failed);
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
    int leftRightDifference(BinaryTreeNode<T>? node) {
        int leftHeight = 0;
        int rightHeight = 0;

        if (node == null) return 0;
        if (node.left != null) leftHeight = node.left.Height();
        if (node.right != null) rightHeight = node.right.Height();
        
        return leftHeight - rightHeight; 
    }

    private BinaryTreeNode<T> RotateRightRight(BinaryTreeNode<T> parent) {
        BinaryTreeNode<T> newParent = parent.right!;
        parent.right = newParent.left;
        newParent.left = parent;
        return newParent;
    }

    private BinaryTreeNode<T> RotateRightLeft(BinaryTreeNode<T> parent) {
        BinaryTreeNode<T> newParent = parent.right!;
        parent.right = RotateLeftLeft(newParent);
        return RotateRightRight(parent);
    }

    private BinaryTreeNode<T> RotateLeftLeft(BinaryTreeNode<T> parent) {
        BinaryTreeNode<T> newParent = parent.left!;
        parent.left = newParent.right;
        newParent.right = parent;
        return newParent;
    }

    private BinaryTreeNode<T> RotateLeftRight(BinaryTreeNode<T> parent) {
        BinaryTreeNode<T> pivot = parent.left!;
        parent.left = RotateRightRight(pivot);
        return RotateLeftLeft(parent);
    }

    public IEnumerable<T> InOrder() {
        foreach (T value in TraverseInOrderRecursive(root)) {
            yield return value;
        }
    }

    private IEnumerable<T> TraverseInOrderRecursive(BinaryTreeNode<T>? currentNode) {
        if (currentNode is not null) {
            foreach (T value in TraverseInOrderRecursive(currentNode.left)) {
                yield return value;
            }
            yield return currentNode.GetValue();
            foreach (T value in TraverseInOrderRecursive(currentNode.right)) {
                yield return value;
            }
        }
    }

    public IEnumerable<T> PostOrder() {
        foreach (T value in TraversePostOrderRecursive(root)) {
            yield return value;
        }
    }

    private IEnumerable<T> TraversePostOrderRecursive(BinaryTreeNode<T>? currentNode) {
        if (currentNode is not null) {
            foreach (T value in TraversePostOrderRecursive(currentNode.left)) {
                yield return value;
            }
            foreach (T value in TraversePostOrderRecursive(currentNode.right)) {
                yield return value;
            }
            yield return currentNode.GetValue();
        }
    }

    public IEnumerable<T> PreOrder() {
        foreach (T value in TraversePreOrderRecursive(root)) {
            yield return value;
        }
    }

    private IEnumerable<T> TraversePreOrderRecursive(BinaryTreeNode<T>? currentNode) {
        if (currentNode is not null) {
            yield return currentNode.GetValue();
            foreach (T value in TraversePreOrderRecursive(currentNode.left)) {
                yield return value;
            }
            foreach (T value in TraversePreOrderRecursive(currentNode.right)) {
                yield return value;
            }
        }
    }

    public int Size() {
        if (root != null) {
            return root.Size();
        } else {
            return 0;
        }
    }
}
