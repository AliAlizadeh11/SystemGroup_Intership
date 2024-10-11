using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1
{
using System;
using System.Collections.Generic;

public class Heap<T> where T : IComparable<T>, IPrintable
{
    private List<T> _elements = new List<T>();

    public int Size => _elements.Count;

    public bool IsEmpty => _elements.Count == 0;

    public void Insert(T item)
    {
        _elements.Add(item);
        HeapifyUp(_elements.Count - 1);
    }

    public T ExtractMax()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");

        T max = _elements[0];
        _elements[0] = _elements[_elements.Count - 1];
        _elements.RemoveAt(_elements.Count - 1);
        HeapifyDown(0);

        return max;
    }

    public T PeekMax()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");

        return _elements[0];
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && _elements[index].CompareTo(_elements[Parent(index)]) > 0)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        int left = LeftChild(index);
        int right = RightChild(index);
        int largest = index;

        if (left < _elements.Count && _elements[left].CompareTo(_elements[largest]) > 0)
        {
            largest = left;
        }

        if (right < _elements.Count && _elements[right].CompareTo(_elements[largest]) > 0)
        {
            largest = right;
        }

        if (largest != index)
        {
            Swap(index, largest);
            HeapifyDown(largest);
        }
    }

    private void Swap(int index1, int index2)
    {
        T temp = _elements[index1];
        _elements[index1] = _elements[index2];
        _elements[index2] = temp;
    }

    private int Parent(int index) => (index - 1) / 2;

    private int LeftChild(int index) => 2 * index + 1;

    private int RightChild(int index) => 2 * index + 2;

    public void PrintHeap()
    {
        foreach (var item in _elements)
        {
            item.Print();
        }
    }

    public void PrintTree()
    {
        PrintTree(0, "", true);
    }

    private void PrintTree(int index, string indent, bool last)
    {
        if (index < _elements.Count)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }
            _elements[index].Print();
            PrintTree(LeftChild(index), indent, false);
            PrintTree(RightChild(index), indent, true);
        }
    }
}
}