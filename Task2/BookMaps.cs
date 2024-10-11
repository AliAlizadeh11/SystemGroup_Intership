using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Task2
{
    public class AudioBookMap : ClassMap<AudioBook>
    {
        public AudioBookMap()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Title).Name("Title");
            Map(m => m.Description).Name("Description");
            Map(m => m.Author).Name("Author");
            Map(m => m.Price).Name("Price");
            Map(m => m.SizeInBytes).Name("SizeInBytes");
            Map(m => m.Duration).Name("TotalMinutes").TypeConverter<MinutesToTimeSpanConverter>();        }
    }

    public class EbookMap : ClassMap<Ebook>
    {
        public EbookMap()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Title).Name("Title");
            Map(m => m.Description).Name("Description");
            Map(m => m.Author).Name("Author");
            Map(m => m.Price).Name("Price");
            Map(m => m.SizeInBytes).Name("SizeInBytes");
        }
    }

    public class PaperBookMap : ClassMap<PaperBook>
    {
        public PaperBookMap()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Title).Name("Title");
            Map(m => m.Description).Name("Description");
            Map(m => m.Author).Name("Author");
            Map(m => m.Price).Name("Price");
            Map(m => m.Weight).Name("Weight");
        }
    }

    public class OrderItemDtoMap : ClassMap<OrderItemDto>
    {
        public OrderItemDtoMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.OrderId).Name("OrderId");
            Map(m => m.BookID).Name("BookID");
            Map(m => m.Quantity).Name("Quantity");
        }
    }

    public class MinutesToTimeSpanConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text != null && double.TryParse(text, out var minutes))
            {
                return TimeSpan.FromMinutes(minutes);
            }
            return TimeSpan.Zero;
        }
    }
}
