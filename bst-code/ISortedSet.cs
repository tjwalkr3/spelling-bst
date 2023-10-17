namespace bst_code;

public interface ISortedSet<T> where T: IComparable<T> {
    public int Size();
    public bool Add(T item);
    public T Remove(T item);
    public T Find(T item);
}