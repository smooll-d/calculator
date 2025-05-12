namespace Calculator;

public interface INodeVisitor<T>
{
    T VisitLiteral(LiteralNode node);
    T VisitBinary(BinaryNode node);
    T VisitUnary(UnaryNode node);
}
