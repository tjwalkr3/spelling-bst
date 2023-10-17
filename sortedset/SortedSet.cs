namespace sortedset;
using bst_code;

public interface ISortedSet<T> where T: IComparable<T> {
    public int Size();
    public bool Add(T item);
    public T Remove(T item);
    public T Find(T item);
}

public class BSTSortedSet<T> : ISortedSet<T> where T: IComparable<T> {
    private BinaryTree<T> data = new BinaryTree<T>();

    public int Size() {
        return data.Size();
    }

    public bool Add(T item) {
        int oldSize = data.Size();
        data.Add(item);
        if (data.Size() > oldSize) {
            return true;
        } else {
            return false;
        }
    }

    public T Remove(T item) {
        int oldSize = data.Size();
        data.Remove(item);
        if (data.Size() < oldSize) {
            return item;
        } else {
            throw new KeyNotFoundException("Item could not be found in this SortedSet");
        }
    }

    public T Find(T item) {
        if (data.Find(item)) {
            return item;
        } else {
            throw new KeyNotFoundException("Item could not be found in this SortedSet");
        }
    }
}
