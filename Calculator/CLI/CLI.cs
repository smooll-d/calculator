namespace Calculator;

public class CLI
{
    private readonly string[] _arguments = Environment.GetCommandLineArgs();

    private bool _visualizeExpression = false;
    private bool _showTokens = false;

    public bool VisualizeExpression { get => _visualizeExpression; }
    public bool ShowTokens { get => _showTokens; }

    public void ProcessArguments()
    {
        if (_arguments.Length >= 2)
        {
            if (_arguments[1] is "--help" or "-h")
            {
                Help();
            }
            else if (_arguments[1] is "--version" or "-v")
            {
                Version();
            }
            else
            {
                bool recognized = false;

                if (_arguments.Contains("--visualize-expression") || _arguments.Contains("-e"))
                {
                    _visualizeExpression = true;
                    recognized = true;
                }

                if (_arguments.Contains("--show-tokens") || _arguments.Contains("-t"))
                {
                    _showTokens = true;
                    recognized = true;
                }

                if (!recognized)
                {
                    Console.WriteLine("Invalid flag. Check '--help' or '-h' for all available flags.");

                    Environment.Exit(1);
                }
            }
        }
    }

    private void Help()
    {
        Console.WriteLine(@"Usage: Calculator [FLAGS]

FLAGS:
    --help,                 -h Show this message and exit.
    --version,              -v Show version and exit.
    --visualize-expression, -e Show the structure of expression as a tree under result.
    --show-tokens,          -t Show expression as tokens under result.");

        Environment.Exit(0);
    }

    private void Version()
    {
        Console.WriteLine("Calculator v1.0.0");
        Console.WriteLine("Made for fun by @smooll-d.");

        Environment.Exit(0);
    }
}
