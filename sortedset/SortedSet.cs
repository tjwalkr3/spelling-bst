namespace sortedset;
using bst_code;

public interface ISortedSet<T> where T: IComparable<T> {
    public void Size();
    public void Add(T item);
    public T Remove(T item);
}

public class SortedSet
{

}
