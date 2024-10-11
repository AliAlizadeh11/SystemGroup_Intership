using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;

namespace Task2
{
    public class ReadCsv
    {
        public IEnumerable<AudioBook> ReadAudioBooks(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });
            csv.Context.RegisterClassMap<AudioBookMap>();
            return csv.GetRecords<AudioBook>().ToList();
        }


    }
}
