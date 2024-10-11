using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2
{
public class PaperBook : Book
{
    public float Weight { get; private init; }
    
    public PaperBook(long id, string title, string description, string author, decimal price, float weight)
            : base(id, title, description, author, price)
        {
            Weight = weight;
        }

}
}
