using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task2
{
public abstract class Book
{
        protected Book(long id, string title, string description, string author, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
            Price = price;
        }

    public long Id { get; init; }
    public string Title { get; private init; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Author { get; private init; }


    public void ChangePrice(decimal price)
    {
        Price = price;
    }

    }
}
