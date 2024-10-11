using System;
using System.Collections.Generic;
using System.Linq;
using FinalProject.Common;
using FinalProject.Data;
using FinalProject.DynamicQuery;

namespace FinalProject;

public class Program
{
    public static void Main()
    {
        using (IDbContext dbContext = new AppDbContext())
        {
            // Through the dbContext instance you can access to entities and perform queries like below:
              // var emails = dbContext.Customers.Select(c => c.Email).ToList();
              // foreach (var email in emails)
              // {
              //     Console.WriteLine(email);
              // }
              
            
            // part1.1
            // Retrieve the count, sum, and average of the total amounts from invoices using a single query
            var invoiceStates = dbContext.Invoices
                .GroupBy(i => 1)
                .Select(g => new
                {
                    Count = g.Count(),
                    Sum = g.Sum(i => i.Total),
                    Average = g.Average(i => i.Total)
                })
                .FirstOrDefault();
            
            Console.WriteLine("Part 1.1");
            if (invoiceStates != null)
            {
                Console.WriteLine($"Count: {invoiceStates.Count}");
                Console.WriteLine($"Sum: {invoiceStates.Sum}");
                Console.WriteLine($"Average: {invoiceStates.Average}");
            }
            Console.WriteLine("----------");
            
            // part1.2
            // Fetch a random selection of the top 100 tracks
            var top100Tracks = dbContext.Tracks
                .OrderBy(t => t.TrackId)
                .Take(100)
                .ToList();
            
            var random = new Random();
            var randomSelection = top100Tracks
                .OrderBy(t => random.Next())
                .ToList();
            
            Console.WriteLine("Part 1.2");
            foreach (var track in randomSelection)
            {
                Console.WriteLine($"TrackId: {track.TrackId}, Name: {track.Name}");
            }
            Console.WriteLine("----------");
            
            // part1.3
            // Retrieve the top 5 tracks that have the highest sales
            var top5Tracks = dbContext.InvoiceLines
                .GroupBy(il => il.TrackId)
                .Select(g => new 
                {
                    TrackId = g.Key,
                    TotalSales = g.Sum(il => il.UnitPrice * il.Quantity)
                })
                .OrderByDescending(g => g.TotalSales)
                .Take(5)
                .Join(dbContext.Tracks,
                    g => g.TrackId,
                    t => t.TrackId,
                    (g, t) => new
                    {
                        t.TrackId,
                        t.Name,
                        g.TotalSales
                    })
                .ToList();
            
            Console.WriteLine("Part 1.3");
            foreach (var track in top5Tracks)
            {
                Console.WriteLine($"TrackId: {track.TrackId}, Name: {track.Name}, Total Sales: {track.TotalSales}");
            }
            Console.WriteLine("----------");

            // part1.4
            // Obtain all customers with invoices containing at least one line item priced less than $2
            var customersWithLowPricedItems = dbContext.InvoiceLines
                .Where(il => il.UnitPrice < 2)
                .Join(dbContext.Invoices,
                    il => il.InvoiceId,
                    inv => inv.InvoiceId,
                    (il, inv) => inv.CustomerId)
                .Distinct()
                .Join(dbContext.Customers,
                    customerId => customerId,
                    customer => customer.CustomerId,
                    (customerId, customer) => customer)
                .ToList();
            
            Console.WriteLine("Part 1.4");
            foreach (var customer in customersWithLowPricedItems)
            {
                Console.WriteLine($"CustomerId: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
            }
            Console.WriteLine("----------");

            // part1.5
            // Retrieve the top 5 loyal customers based on the total purchase amount across all their invoices.
            var top5LoyalCustomers = dbContext.Invoices
                .GroupBy(i => i.CustomerId)
                .Select(g => new 
                {
                    CustomerId = g.Key,
                    TotalPurchaseAmount = g.Sum(i => i.Total)
                })
                .OrderByDescending(g => g.TotalPurchaseAmount)
                .Take(5)
                .Join(dbContext.Customers,
                    g => g.CustomerId,
                    c => c.CustomerId,
                    (g, c) => new
                    {
                        c.CustomerId,
                        c.FirstName,
                        c.LastName,
                        c.Email,
                        g.TotalPurchaseAmount
                    })
                .ToList();
            
            Console.WriteLine("Part 1.5");
            foreach (var customer in top5LoyalCustomers)
            {
                Console.WriteLine($"CustomerId: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Total Purchase Amount: {customer.TotalPurchaseAmount}");
            }
            Console.WriteLine("----------");
            
            
            //part1.6
            //Retrieve a list of employees along with their respective managers
            var employeesWithManagers = dbContext.Employees
                .GroupJoin(
                    dbContext.Employees,
                    e => e.ReportsTo,
                    m => m.EmployeeId,
                    (e, managers) => new { Employee = e, Managers = managers }
                )
                .SelectMany(
                    em => em.Managers.DefaultIfEmpty(),
                    (em, m) => new
                    {
                        EmployeeId = em.Employee.EmployeeId,
                        EmployeeLastName = em.Employee.LastName,
                        EmployeeFirstName = em.Employee.FirstName,
                        EmployeeTitle = em.Employee.Title,
                        ManagerLastName = m.LastName,
                        ManagerFirstName = m.FirstName
                    }
                )
                .ToList();
            
            Console.WriteLine("Part 1.6");
            foreach (var item in employeesWithManagers)
            {
                Console.WriteLine($"EmployeeId: {item.EmployeeId}, Employee: {item.EmployeeFirstName} {item.EmployeeLastName}, Title: {item.EmployeeTitle}, Manager: {item.ManagerFirstName} {item.ManagerLastName}");
            }
            Console.WriteLine("----------");
            
            // part1.7
            //  Calculate the average sales per month.
            var monthlySales = dbContext.Invoices
                .GroupBy(i => new { i.InvoiceDate.Year, i.InvoiceDate.Month })
                .Select(g => new 
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSales = g.Sum(i => i.Total)
                })
                .ToList();
            
            var averageSalesPerMonth = monthlySales
                .GroupBy(ms => ms.Month)
                .Select(g => new 
                {
                    Month = g.Key,
                    AverageSales = g.Average(ms => ms.TotalSales)
                })
                .OrderBy(m => m.Month)
                .ToList();
            
            Console.WriteLine("Part 1.7");
            foreach (var monthSales in averageSalesPerMonth)
            {
                string monthName = new DateTime(1, monthSales.Month, 1).ToString("MMMM");
                Console.WriteLine($"Month: {monthName}, Average Sales: {monthSales.AverageSales:C}");
            }
            Console.WriteLine("----------");
            
            
            // part1.8
            // Fetch the top 100 most popular genres for each country
            var topGenresByCountry = dbContext.InvoiceLines
            .Join(dbContext.Invoices, il => il.InvoiceId, i => i.InvoiceId,
                    (il, i) => new { il, i })
                .Join(dbContext.Tracks, ili => ili.il.TrackId, t => t.TrackId,
                    (ili, t) => new { ili.il, ili.i, t })
                .Join(dbContext.Genres, ilt => ilt.t.GenreId, g => g.GenreId,
                    (ilt, g) => new { ilt.il, ilt.i, ilt.t, g })
                .GroupBy(x => new { x.i.BillingCountry, x.g.GenreId, x.g.Name })
                .Select(g => new 
                {
                    Country = g.Key.BillingCountry,
                    Genre = g.Key.Name,
                    GenreId = g.Key.GenreId,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .GroupBy(x => x.Country)
                .AsEnumerable()
                .SelectMany(g => g.Take(100))
                .ToList();
            
            Console.WriteLine("Part 1.8");
            foreach (var item in topGenresByCountry)
            {
                Console.WriteLine($"Country: {item.Country}, Genre: {item.Genre}, Count: {item.Count}");
            }
            Console.WriteLine("----------");
            

            // part1.9
            // Retrieve the top 100 most popular tracks based on customer preferences and purchases.
            
            var topTracks = dbContext.InvoiceLines
                .Join(dbContext.Tracks, il => il.TrackId, t => t.TrackId,
                    (il, t) => new { il, t }) 
                .GroupBy(x => new { x.t.TrackId, x.t.Name })
                .Select(g => new 
                {
                    TrackId = g.Key.TrackId,
                    TrackName = g.Key.Name,
                    PurchaseCount = g.Count() 
                })
                .OrderByDescending(x => x.PurchaseCount)
                .Take(100) 
                .ToList();
            
            Console.WriteLine("Part 1.9");
            foreach (var track in topTracks)
            {
                Console.WriteLine($"Track ID: {track.TrackId}, Track Name: {track.TrackName}, Purchase Count: {track.PurchaseCount}");
            }
            Console.WriteLine("----------");

            // part1.10
            // Obtain a list of all customers and details of their last invoice, displaying null when a customer has not made any purchases
            
            var customerInvoices = dbContext.Customers
                .GroupJoin(
                    dbContext.Invoices,
                    customer => customer.CustomerId,
                    invoice => invoice.CustomerId,
                    (customer, invoices) => new { customer, invoices })
                .SelectMany(
                    x => x.invoices.DefaultIfEmpty(),
                    (x, invoice) => new { x.customer, invoice })
                .GroupBy(x => x.customer)
                .Select(g => new
                {
                    CustomerId = g.Key.CustomerId,
                    CustomerName = g.Key.FirstName + " " + g.Key.LastName,
                    LastInvoice = g.OrderByDescending(i => i.invoice.InvoiceDate).FirstOrDefault().invoice
                })
                .ToList();
            
            Console.WriteLine("Part 1.10");
            foreach (var item in customerInvoices)
            {
                Console.WriteLine($"Customer ID: {item.CustomerId}, Customer Name: {item.CustomerName}");
                if (item.LastInvoice != null)
                {
                    Console.WriteLine(
                        $"Last Invoice ID: {item.LastInvoice.InvoiceId}, Invoice Date: {item.LastInvoice.InvoiceDate}");
                }
                else
                {
                    Console.WriteLine("No purchases made.");
                }
            
            }
            Console.WriteLine("----------");
            
            
            // part2

            // Example user inputs
            var queryParameters = new QueryParameters
            {
                Columns = new[] { "CustomerId", "FirstName", "LastName", "Country" },
                Filters = new List<Filter>
                {
                    new Filter { PropertyName = "Country", Operation = "Equals", Value = "USA" }
                },
                SortOptions = new List<SortOption>
                {
                    new SortOption { PropertyName = "LastName", IsDescending = false }
                }
            };

            // Apply dynamic query parameters
            var query = DynamicQueryBuilder.ApplyQueryParameters(dbContext.Customers.AsQueryable(), queryParameters);

            // Execute and display results
            var results = query.ToList();
            
            Console.WriteLine("Part 2");
            foreach (var customer in results)
            {
                Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Country: {customer.Country}");
            }
            
            Console.WriteLine("----------");
            
            }
        }
}


// //part3
//
// using System;
// using LoyaltyClub.Plugin;
//
// namespace LoyaltyClub
// {
//     public class Program
//     {
//         public static void Main()
//         {
//             var purchaseData = new CustomerPurchaseData
//             {
//                 TracksPurchased = 3,
//                 PurchaseDate = new DateTime(2024, 4, 15)
//             };
//
//             var calculator = new LoyaltyPointsCalculator();
//             var totalPoints = calculator.CalculateTotalPoints(purchaseData);
//             
//             Console.WriteLine($"Total Loyalty Points: {totalPoints}");
//         }
//     }
// }
