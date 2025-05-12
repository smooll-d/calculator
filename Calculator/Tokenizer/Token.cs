namespace Calculator;

public enum TokenType
{
    UNKNOWN,
    NUMBER,
    ADDITION,
    SUBTRACTION,
    MULTIPLICATION,
    DIVISION,
    EXPONENTIATION,
    LEFT_PARENTHESIS,
    RIGHT_PARENTHESIS
}

public struct Token(string value, TokenType type)
{
    public string Value { get; } = value;

    public TokenType Type { get; } = type;
}
