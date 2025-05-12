namespace Calculator;

public struct NodeBox()
{
    public List<string> Lines { get; set; } = [];

    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public int Center { get; set; } = 0;
};

public class ExpressionVisualizer : INodeVisitor<NodeBox>
{
    public NodeBox VisitLiteral(LiteralNode node)
    {
        NodeBox box = new();

        box.Lines.Add(node.Value.ToString());
        box.Width = box.Lines.Max(line => line.Length);
        box.Height = box.Lines.Count;
        box.Center = box.Width / 2;

        return box;
    }

    public NodeBox VisitBinary(BinaryNode node)
    {
        NodeBox box = new();

        NodeBox left = node.Left.Accept(this);
        NodeBox right = node.Right.Accept(this);

        while (left.Lines.Count < right.Lines.Count)
        {
            left.Lines.Add(new string(' ', left.Width));
        }

        while (right.Lines.Count < left.Lines.Count)
        {
            right.Lines.Add(new string(' ', right.Width));
        }

        box.Height = (left.Lines.Count + right.Lines.Count) / 2;

        string op = new string(' ', left.Width + right.Width + 1);
        char[] ops = op.ToCharArray();
        ops[(left.Width + right.Width) / 2 + (left.Center / 2)] = node.Operator.Value.ToCharArray()[0];
        op = new string(ops);

        box.Lines.Add(op);
        box.Lines.Add("/ ".PadLeft(left.Center + 1) + "\\".PadLeft(right.Center + left.Width));

        for (int i = 0; i < box.Height; i++)
        {
            box.Lines.Add(left.Lines[i].PadLeft(left.Width) + ' ' + right.Lines[i].PadLeft(right.Width + left.Center - 1));
        }

        box.Width = box.Lines.Max(line => line.Length);
        box.Center = box.Width / 2;

        return box;
    }

    public NodeBox VisitUnary(UnaryNode node)
    {
        NodeBox box = new();

        NodeBox right = node.Right.Accept(this);

        string op = node.Operator.Value;
        string separator = "|";
        string value = right.Lines[0];

        box.Lines.Add(op);
        box.Lines.Add(separator);
        box.Lines.Add(value);
        box.Width = box.Lines.Max(line => line.Length);
        box.Height = box.Lines.Count;
        box.Center = right.Width / 2;

        return box;
    }
}
