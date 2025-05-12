namespace Calculator;

public class Tokenizer
{
    private readonly char[] _operatorTokens = ['+', '-', '*', '/', '^', '(', ')'];

    public List<Token> Tokens { get; }

    public Tokenizer(string expression)
    {
        Tokens = [];

        string token = String.Empty;

        TokenType currentTokenType = TokenType.UNKNOWN;

        for (int i = 0; i < expression.Length; i++)
        {
            if (char.IsDigit(expression[i]))
            {
                token += expression[i];
                currentTokenType = TokenType.NUMBER;
            }
            else if (expression[i] == ' ')
            {
                continue;
            }
            else if (expression[i] is '.' or ',')
            {
                token += expression[i];
            }
            else
            {
                token = Flush(token, currentTokenType);

                if (_operatorTokens.Contains(expression[i]))
                {
                    token += expression[i];
                    currentTokenType = ProcessOperator(expression[i]);
                    token = Flush(token, currentTokenType);
                }
                else
                {
                    Console.WriteLine($"\"{expression[i]}\" is not a valid operator.");
                    break;
                }
            }
        }

        _ = Flush(token, currentTokenType);
    }

    public void Print()
    {
        Console.WriteLine('[');

        for (int i = 0; i < Tokens.Count; i++)
        {
            Console.WriteLine("\t{");
            Console.WriteLine($"\t\t\"Value\": \"{Tokens[i].Value}\"");
            Console.WriteLine($"\t\t\"Type\": \"{Tokens[i].Type}\"");
            Console.WriteLine(i < Tokens.Count - 1 ? "\t}," : "\t}");
        }

        Console.WriteLine(']');
    }

    private string Flush(string token, TokenType currentTokenType)
    {
        if (!String.IsNullOrEmpty(token))
        {
            Tokens.Add(new(token, currentTokenType));
        }

        return String.Empty;
    }

    private static TokenType ProcessOperator(char op)
    {
        return op switch
        {
            '+' => TokenType.ADDITION,
            '-' => TokenType.SUBTRACTION,
            '*' => TokenType.MULTIPLICATION,
            '/' => TokenType.DIVISION,
            '^' => TokenType.EXPONENTIATION,
            '(' => TokenType.LEFT_PARENTHESIS,
            ')' => TokenType.RIGHT_PARENTHESIS,
            _ => TokenType.UNKNOWN,
        };
    }
}
