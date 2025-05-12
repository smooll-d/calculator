namespace Calculator;

public class Evaluator : INodeVisitor<double>
{
    public double VisitLiteral(LiteralNode node)
    {
        return node.Value;
    }

    public double VisitBinary(BinaryNode node)
    {
        double left = node.Left.Accept(this);
        double right = node.Right.Accept(this);

        return node.Operator.Type switch
        {
            TokenType.ADDITION => left + right,
            TokenType.SUBTRACTION => left - right,
            TokenType.MULTIPLICATION => left * right,
            TokenType.DIVISION => left / right,
            TokenType.EXPONENTIATION => Math.Pow(left, right),
            _ => throw new InvalidOperationException("Unknown expression.")
        };
    }

    public double VisitUnary(UnaryNode node)
    {
        double right = node.Right.Accept(this);

        return node.Operator.Type switch
        {
            TokenType.ADDITION => right,
            TokenType.SUBTRACTION => -right,
            _ => throw new InvalidOperationException("Unknown unary operator.")
        };
    }
}
