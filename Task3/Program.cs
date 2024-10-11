using System;
using System.Collections;
using System.Collections.Generic;

public class MyCollection : IEnumerable<int>
{
    private List<int> _items = new List<int>();

    public void Add(int item)
    {
        _items.Add(item);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Program
{
    public static void Main()
    {
        MyCollection collection = new MyCollection();
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);

        foreach (int item in collection)
        {
            Console.WriteLine(item);
        }
    }
}
