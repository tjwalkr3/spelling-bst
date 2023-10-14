namespace bst_code;

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