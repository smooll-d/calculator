//TODO: Add custom printing of nodes
//TODO: Add error handling to parser

using Calculator;

CLI cli = new();

cli.ProcessArguments();

while (true)
{
    Console.Write("> ");
    string? readResult = Console.ReadLine();

    if (readResult?.ToLower() is "exit" or "quit" or "q")
    {
        break;
    }
    else if (readResult == null || readResult == String.Empty)
    {
        Console.WriteLine("Please input an expression.");
    }
    else
    {
        try
        {
            Parser parser = new(readResult);

            parser.PrintEvaluation();

            if (cli.VisualizeExpression)
            {
                parser.PrintTree();
            }

            if (cli.ShowTokens)
            {
                parser.PrintTokens();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
