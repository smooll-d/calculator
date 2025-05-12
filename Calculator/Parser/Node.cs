namespace Calculator;

public abstract class Node
{
    public abstract T Accept<T>(INodeVisitor<T> visitor);
}

public class LiteralNode(double value) : Node
{
    public double Value { get; } = value;

    public override T Accept<T>(INodeVisitor<T> visitor)
    {
        return visitor.VisitLiteral(this);
    }
}

public class BinaryNode(Node left, Token op, Node right) : Node
{
    public Node Left { get; } = left;
    public Token Operator { get; } = op;
    public Node Right { get; } = right;

    public override T Accept<T>(INodeVisitor<T> visitor)
    {
        return visitor.VisitBinary(this);
    }
}

public class UnaryNode(Token op, Node right) : Node
{
    public Token Operator { get; } = op;
    public Node Right { get; } = right;

    public override T Accept<T>(INodeVisitor<T> visitor)
    {
        return visitor.VisitUnary(this);
    }
}
