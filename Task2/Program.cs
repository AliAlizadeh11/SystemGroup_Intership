using System;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var readCsv = new ReadCsv();

            try
            {
                var audioBooks = readCsv.ReadAudioBooks("AudioBooks.csv");
                

                Console.WriteLine("AudioBooks:");
                foreach (var book in audioBooks)
                {
                    Console.WriteLine($"{book.Id}, {book.Title}, {book.Author}, Duration: {book.Duration}");
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
