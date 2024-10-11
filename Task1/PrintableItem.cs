using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1
{

public class PrintableItem : IComparable<PrintableItem>, IPrintable
{
    public int Value { get; set; }

    public PrintableItem(int value)
    {
        Value = value;
    }

    public int CompareTo(PrintableItem other)
    {
        return Value.CompareTo(other.Value);
    }

    public void Print()
    {
        Console.WriteLine(Value);
    }
}
}