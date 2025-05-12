using System.Data;

namespace Calculator;

public class Parser
{
    private readonly Tokenizer _tokenizer;

    private int _currentTokenIndex;

    private readonly Evaluator evaluator = new();
    private readonly ExpressionVisualizer expressionVisualizer = new();

    private string _expression;

    private Node _AST { get; }

    public Parser(string expression)
    {
        _expression = expression;

        _tokenizer = new(expression);

        _AST = Expression();
    }

    public void PrintTokens()
    {
        _tokenizer.Print();
    }

    public void PrintTree()
    {
        NodeBox tree = _AST.Accept(expressionVisualizer);

        Console.WriteLine("\nExpression Tree:\n");
        Console.WriteLine(String.Join('\n', tree.Lines));
        Console.WriteLine();
    }

    public void PrintEvaluation()
    {
        double result = _AST.Accept(evaluator);

        Console.WriteLine(_expression + " = " + result);
    }

    private Node Expression()
    {
        Node left = Term();

        while (Match(TokenType.ADDITION, TokenType.SUBTRACTION))
        {
            Token op = Previous();

            Node right = Term();

            left = new BinaryNode(left, op, right);
        }

        return left;
    }

    private Node Term()
    {
        Node left = Power();

        while (Match(TokenType.MULTIPLICATION, TokenType.DIVISION))
        {
            Token op = Previous();

            Node right = Power();

            left = new BinaryNode(left, op, right);
        }

        return left;
    }

    private Node Power()
    {
        Node left = Unary();

        if (Match(TokenType.EXPONENTIATION))
        {
            Token op = Previous();

            Node right = Power();

            left = new BinaryNode(left, op, right);
        }

        return left;
    }

    private Node Unary()
    {
        if (Match(TokenType.ADDITION, TokenType.SUBTRACTION))
        {
            Token op = Previous();

            Node right = Unary();

            return new UnaryNode(op, right);
        }

        return Factor();
    }

    private Node Factor()
    {
        if (Match(TokenType.NUMBER))
        {
            return new LiteralNode(Convert.ToDouble(Previous().Value));
        }
        else if (Match(TokenType.LEFT_PARENTHESIS))
        {
            Node expression = Expression();

            _ = Expect(TokenType.RIGHT_PARENTHESIS);

            return expression;
        }

        throw new SyntaxErrorException($"Unexpected symbol: {Peek().Value}");
    }

    // Courtesy of https://www.craftinginterpreters.com/parsing-expressions.html
    private bool Match(params TokenType[] tokenTypes)
    {
        foreach (TokenType tokenType in tokenTypes)
        {
            if (Check(tokenType))
            {
                _ = Advance();

                return true;
            }
        }

        return false;
    }

    private bool Expect(TokenType tokenType)
    {
        if (Match(tokenType))
        {
            return true;
        }

        Console.WriteLine($"Unexpected symbol: {Peek().Value}");

        return false;
    }

    // Courtesy of https://www.craftinginterpreters.com/parsing-expressions.html
    private bool Check(TokenType tokenType)
    {
        return !IsAtEnd() && tokenType == Peek().Type;
    }

    // Courtesy of https://www.craftinginterpreters.com/parsing-expressions.html
    private Token Advance()
    {
        if (!IsAtEnd())
        {
            _currentTokenIndex++;
        }

        return Previous();
    }

    // Courtesy of https://www.craftinginterpreters.com/parsing-expressions.html
    private bool IsAtEnd()
    {
        return _currentTokenIndex == _tokenizer.Tokens.Count;
    }

    // Courtesy of https://www.craftinginterpreters.com/parsing-expressions.html
    private Token Peek()
    {
        return _tokenizer.Tokens[_currentTokenIndex];
    }

    // Courtesy of https://www.craftinginterpreters.com/parsing-expressions.html
    private Token Previous()
    {
        return _tokenizer.Tokens[_currentTokenIndex - 1];
    }
}
