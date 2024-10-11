using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2
{
public class Ebook : Book
{
    public long SizeInBytes { get; private init; }

    public Ebook(long id, string title, string description, string author, decimal price, long sizeInBytes)
            : base(id, title, description, author, price)
        {
            SizeInBytes = sizeInBytes;
        }

    }
}
