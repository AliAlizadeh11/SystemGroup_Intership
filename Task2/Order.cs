using System;
using System.Collections.Generic;

namespace Task2
{
    public class Order
    {
        private readonly List<OrderItem> _items = new List<OrderItem>();

        public long Id { get; private init; }
        public DateTime DateTime { get; private init; }
        public decimal TotalPrice { get; private set; }
        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

        public Order(long id, DateTime dateTime, decimal totalPrice)
        {
            Id = id;
            DateTime = dateTime;
            TotalPrice = totalPrice;
        }

        public void Add(Book book, int quantity)
        {
            if (_items.Count > 4)
            {
                throw new InvalidOperationException("Cannot add more than 5 items to the order.");
            }

            var item = new OrderItem(book, quantity);
            _items.Add(item);

            TotalPrice += item.Book.Price * quantity;
        }
    }

    public class OrderItem
    {
        public Book Book { get; private init; }
        public int Quantity { get; private init; }

        public OrderItem(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;
        }
    }
}
