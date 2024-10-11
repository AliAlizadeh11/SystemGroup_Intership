using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2
{
public class AudioBook : Book
{
    public long SizeInBytes { get; set; }
    public TimeSpan Duration { get; set; }
    public AudioBook(long id, string title, string description, string author, decimal price, long sizeInBytes, TimeSpan duration)
            : base(id, title, description, author, price)
        {
            SizeInBytes = sizeInBytes;
            Duration = duration;
        }

}
}
