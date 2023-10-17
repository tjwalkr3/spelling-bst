namespace bst_code;

public interface ITraversable<T> {
    IEnumerable<T> PreOrder();
    IEnumerable<T> InOrder();
    IEnumerable<T> PostOrder();
}