using System;

namespace Task1{
class Program
{
    public static void Main(string[] args)
    {
        Heap<PrintableItem> maxHeap = new Heap<PrintableItem>();
        maxHeap.Insert(new PrintableItem(10));
        maxHeap.Insert(new PrintableItem(4));
        maxHeap.Insert(new PrintableItem(15));
        maxHeap.Insert(new PrintableItem(3));
        maxHeap.Insert(new PrintableItem(20));
        maxHeap.Insert(new PrintableItem(12));
        maxHeap.Insert(new PrintableItem(7));
        maxHeap.Insert(new PrintableItem(2));

        Console.WriteLine("Heap elements:");
        maxHeap.PrintHeap();

        Console.WriteLine("\nHeap as a tree:");
        maxHeap.PrintTree();

        Console.WriteLine($"\nMax element: {maxHeap.ExtractMax().Value}");
        Console.WriteLine("Heap after extracting max element:");
        maxHeap.PrintHeap();

        Console.WriteLine("\nHeap as a tree after extracting max element:");
        maxHeap.PrintTree();
    }
}

}