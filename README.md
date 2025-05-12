# calculator
Simple calculator made in C#.

# Features
- Support for basic operators:

| Operator       | Operation            |
|--------------- | -------------------- |
| `+`            | Addition             |
| `-`            | Subtraction/Negation |
| `*`            | Multiplication       |
| `/`            | Division             |
| `^`            | Exponentiation       |
| `()`           | Parentheses          |

- Calculation follows PEMDAS
- Basic CLI with some cool options
- Tokenization visualization
- "Basic" expression visualization (with big expressions, it'll start to fail a little bit, but it's still readable)

# Building
> [!TIP]
> You can get a binary from the Releases page.

For building you need .NET 9.

> [!NOTE]
> I'll only be showcasing, how to build the project on Linux, but for Windows, you should only need to open the `.sln` file in Visual Studio and build the solution. There aren't any external dependencies. Just install .NET 9 and the appropriate C# dev stuff in VS.

After cloning the repo. Go into the directory and run:

```bash
dotnet build -c Release
```

After it's built, to run it:

```bash
bin/Release/net9.0/Calculator
```

That's it. You can also add `--help` to the above command to see the help and how to use the visualization options.
