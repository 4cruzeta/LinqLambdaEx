using System;
using System.Linq;
using LinqLambdaEx.Entities;
using System.Collections.Generic;

namespace LinqLambdaEx
{
    class Program
    {

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {

            CategoryClass c1 = new CategoryClass() { Id = 1, Name = "Tools", Tier = 2 };
            CategoryClass c2 = new CategoryClass() { Id = 2, Name = "Computers", Tier = 1 };
            CategoryClass c3 = new CategoryClass() { Id = 3, Name = "Electronics", Tier = 1 };

            List<ProductClass> ProductClasss = new List<ProductClass>() {
                new ProductClass() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new ProductClass() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new ProductClass() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new ProductClass() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new ProductClass() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new ProductClass() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new ProductClass() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new ProductClass() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new ProductClass() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new ProductClass() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new ProductClass() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            var r1 = ProductClasss.Where(p => p.Category.Tier == 1 && p.Price < 900.0);
            Print("TIER 1 AND PRICE < 900:", r1);

            var r2 = ProductClasss.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            Print("NAMES OF ProductClassS FROM TOOLS", r2);

            var r3 = ProductClasss.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryClassName = p.Category.Name });
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT", r3);

            var r4 = ProductClasss.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4", r5);

            var r6 = ProductClasss.FirstOrDefault();
            Console.WriteLine("First or default test1: " + r6);
            var r7 = ProductClasss.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("First or default test2: " + r7);
            Console.WriteLine();

            var r8 = ProductClasss.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or default test1: " + r8);
            var r9 = ProductClasss.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("Single or default test2: " + r9);
            Console.WriteLine();

            var r10 = ProductClasss.Max(p => p.Price);
            Console.WriteLine("Max price: " + r10);
            var r11 = ProductClasss.Min(p => p.Price);
            Console.WriteLine("Min price: " + r11);
            var r12 = ProductClasss.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("CategoryClass 1 Sum prices: " + r12);
            var r13 = ProductClasss.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("CategoryClass 1 Average prices: " + r13);
            var r14 = ProductClasss.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("CategoryClass 5 Average prices: " + r14);
            var r15 = ProductClasss.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate((x, y) => x + y);
            Console.WriteLine("CategoryClass 1 Average prices: " + r15);
            var r16 = ProductClasss.Where(p => p.Category.Id == 5).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y); //tradada excessão
            Console.WriteLine("CategoryClass 5 (no category 5!) aggregate sum: " + r16);
            Console.WriteLine();

            var r17 = ProductClasss.GroupBy(p => p.Category);
            foreach (IGrouping<CategoryClass, ProductClass> group in r17)
            {
                Console.WriteLine("CategoryClass " + group.Key.Name + ":");
                foreach (ProductClass p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}
