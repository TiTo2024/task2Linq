using System;
using System.Collections.Generic;
using System.Linq;
using static LINQAssignment.ListGenerators;

namespace LINQAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Restriction Operators
            Console.WriteLine("=== LINQ - Restriction Operators ===");

            // 1. Find all products that are out of stock.
            var outOfStockProducts = ProductList.Where(p => p.UnitsInStock == 0);
            Console.WriteLine("\n1. Out of Stock Products:");
            foreach (var p in outOfStockProducts)
                Console.WriteLine($" - {p.ProductName} (Stock: {p.UnitsInStock})");

            // 2. Find all products that are in stock and cost more than 3.00 per unit.
            var inStockExpensiveProducts = ProductList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m);
            Console.WriteLine("\n2. In Stock & Price > 3.00:");
            foreach (var p in inStockExpensiveProducts)
                Console.WriteLine($" - {p.ProductName} | Price: {p.UnitPrice:C} | Stock: {p.UnitsInStock}");

            // 3. Returns digits whose name is shorter than their value.
            string[] digitsArr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNamedDigits = digitsArr.Where((name, index) => name.Length < index);
            Console.WriteLine("\n3. Digits with name shorter than value:");
            foreach (var d in shortNamedDigits)
                Console.WriteLine($" - {d}");

            #endregion

            #region LINQ - Element Operators
            Console.WriteLine("\n=== LINQ - Element Operators ===");

            // 1. Get first Product out of Stock
            var firstOutOfStock = ProductList.FirstOrDefault(p => p.UnitsInStock == 0);
            Console.WriteLine($"\n1. First Out of Stock Product: {firstOutOfStock?.ProductName ?? "None"}");

            // 2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned.
            var expensiveProduct = ProductList.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine($"2. First Product Price > 1000: {expensiveProduct?.ProductName ?? "Null (Not Found)"}");

            // 3. Retrieve the second number greater than 5
            int[] numbersArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThanFive = numbersArr.Where(n => n > 5).ElementAtOrDefault(1);
            Console.WriteLine($"3. Second number greater than 5: {secondGreaterThanFive}");

            #endregion

            #region LINQ - Aggregate Operators
            Console.WriteLine("\n=== LINQ - Aggregate Operators ===");

            int[] aggregateArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 1. Uses Count to get the number of odd numbers in the array.
            int oddNumbersCount = aggregateArr.Count(n => n % 2 != 0);
            Console.WriteLine($"\n1. Odd numbers count: {oddNumbersCount}");

            // 3. Return a list of categories and how many products each has.
            var categoryProductCounts = ProductList.GroupBy(p => p.Category)
                                                   .Select(g => new { Category = g.Key, ProductCount = g.Count() });
            Console.WriteLine("\n3. Category Product Counts:");
            foreach (var item in categoryProductCounts)
                Console.WriteLine($" - {item.Category}: {item.ProductCount} products");

            // 4. Get the total of the numbers in an array.
            int totalSum = aggregateArr.Sum();
            Console.WriteLine($"\n4. Total sum of numbers: {totalSum}");

            // 6. Get the total units in stock for each product category.
            var totalUnitsPerCategory = ProductList.GroupBy(p => p.Category)
                                                   .Select(g => new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) });
            Console.WriteLine("\n6. Total units in stock per category:");
            foreach (var item in totalUnitsPerCategory)
                Console.WriteLine($" - {item.Category}: {item.TotalUnits} units");

            // 8. Get the cheapest price among each category's products.
            var cheapestPricePerCategory = ProductList.GroupBy(p => p.Category)
                                                       .Select(g => new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) });
            Console.WriteLine("\n8. Cheapest price per category:");
            foreach (var item in cheapestPricePerCategory)
                Console.WriteLine($" - {item.Category}: {item.CheapestPrice:C}");

            // 9. Get the products with the cheapest price in each category (Use Let)
            var cheapestProductsPerCategory = from p in ProductList
                                              group p by p.Category into g
                                              let minPrice = g.Min(p => p.UnitPrice)
                                              select new
                                              {
                                                  Category = g.Key,
                                                  CheapestProducts = g.Where(p => p.UnitPrice == minPrice)
                                              };
            Console.WriteLine("\n9. Cheapest products per category:");
            foreach (var group in cheapestProductsPerCategory)
            {
                Console.WriteLine($" Category: {group.Category}");
                foreach (var p in group.CheapestProducts)
                    Console.WriteLine($"   - {p.ProductName} ({p.UnitPrice:C})");
            }

            // 11. Get the most expensive price among each category's products.
            var maxPricePerCategory = ProductList.GroupBy(p => p.Category)
                                                 .Select(g => new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) });
            Console.WriteLine("\n11. Max price per category:");
            foreach (var item in maxPricePerCategory)
                Console.WriteLine($" - {item.Category}: {item.MaxPrice:C}");

            // 12. Get the products with the most expensive price in each category.
            var mostExpensiveProductsPerCategory = from p in ProductList
                                                   group p by p.Category into g
                                                   let maxPrice = g.Max(p => p.UnitPrice)
                                                   select new
                                                   {
                                                       Category = g.Key,
                                                       ExpensiveProducts = g.Where(p => p.UnitPrice == maxPrice)
                                                   };
            Console.WriteLine("\n12. Most expensive products per category:");
            foreach (var group in mostExpensiveProductsPerCategory)
            {
                Console.WriteLine($" Category: {group.Category}");
                foreach (var p in group.ExpensiveProducts)
                    Console.WriteLine($"   - {p.ProductName} ({p.UnitPrice:C})");
            }

            // 14. Get the average price of each category's products.
            var avgPricePerCategory = ProductList.GroupBy(p => p.Category)
                                                 .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.UnitPrice) });
            Console.WriteLine("\n14. Average price per category:");
            foreach (var item in avgPricePerCategory)
                Console.WriteLine($" - {item.Category}: {item.AveragePrice:C}");

            #endregion

            #region LINQ - Ordering Operators
            Console.WriteLine("\n=== LINQ - Ordering Operators ===");

            // 1. Sort a list of products by name.
            var sortedByName = ProductList.OrderBy(p => p.ProductName);
            Console.WriteLine("\n1. Sorted by Name:");
            foreach (var p in sortedByName)
                Console.WriteLine($" - {p.ProductName}");

            // 2. Uses a custom comparer to do a case-insensitive sort of the words in an array.
            string[] wordsArr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "Clover", "cHeRry" };
            var sortedCaseInsensitive = wordsArr.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n2. Case-insensitive sort:");
            Console.WriteLine($" [{string.Join(", ", sortedCaseInsensitive)}]");

            // 3. Sort a list of products by units in stock from highest to lowest.
            var sortedByStockDesc = ProductList.OrderByDescending(p => p.UnitsInStock);
            Console.WriteLine("\n3. Sorted by Stock (Desc):");
            foreach (var p in sortedByStockDesc)
                Console.WriteLine($" - {p.ProductName}: {p.UnitsInStock}");

            // 4. Sort a list of digits, first by length of their name, and then alphabetically by the name itself.
            string[] digitsOrderArr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits = digitsOrderArr.OrderBy(d => d.Length).ThenBy(d => d);
            Console.WriteLine("\n4. Sorted digits by length then name:");
            Console.WriteLine($" [{string.Join(", ", sortedDigits)}]");

            // 5. Sort first by word length and then by a case-insensitive sort of the words in an array.
            var sortedByLenThenInsensitive = wordsArr.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);

            // 6. Sort a list of products, first by category, and then by unit price, from highest to lowest.
            var sortedByCategoryThenPrice = ProductList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);

            // 7. Sort first by word length and then by a case-insensitive descending sort of the words in an array.
            var sortedByLenThenInsensitiveDesc = wordsArr.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);

            // 8. Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array.
            var reversedDigitsWithI = digitsOrderArr.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            Console.WriteLine("\n8. Reversed digits with second letter 'i':");
            Console.WriteLine($" [{string.Join(", ", reversedDigitsWithI)}]");

            #endregion

            #region LINQ - Transformation Operators
            Console.WriteLine("\n=== LINQ - Transformation Operators ===");

            // 1. Return a sequence of just the names of a list of products.
            var productNames = ProductList.Select(p => p.ProductName);

            // 2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).
            string[] wordsTransform = { "aPPLE", "BlueBeRrY", "cHeRry" };
            var upperLowerWords = wordsTransform.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });

            // 3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.
            var productRenamedProps = ProductList.Select(p => new
            {
                p.ProductID,
                p.ProductName,
                Price = p.UnitPrice,
                p.Category
            });

            // 4. Determine if the value of ints in an array match their position in the array.
            int[] matchPosArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var matchPosition = matchPosArr.Select((num, index) => new { Number = num, InPlace = (num == index) });
            Console.WriteLine("\n4. Number In-place check:");
            foreach (var item in matchPosition)
                Console.WriteLine($" {item.Number}: {item.InPlace}");

            // 5. Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.
            int[] numbersA = { 10, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var lessPairs = from a in numbersA
                            from b in numbersB
                            where a < b
                            select new { a, b };
            Console.WriteLine("\n5. Number pairs (a < b):");
            foreach (var pair in lessPairs)
                Console.WriteLine($" {pair.a} is less than {pair.b}");

            #endregion

            #region LINQ - Partitioning Operators
            Console.WriteLine("\n=== LINQ - Partitioning Operators ===");

            int[] partitionArr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
            var takeUntilLessThanPos = partitionArr.TakeWhile((num, index) => num >= index);
            Console.WriteLine($"3. TakeWhile (num >= index): [{string.Join(", ", takeUntilLessThanPos)}]");

            // 4. Get the elements of the array starting from the first element divisible by 3.
            var skipUntilDivisibleBy3 = partitionArr.SkipWhile(num => num % 3 != 0);
            Console.WriteLine($"4. SkipWhile (num % 3 != 0): [{string.Join(", ", skipUntilDivisibleBy3)}]");

            // 5. Get the elements of the array starting from the first element less than its position.
            var skipUntilLessThanPos = partitionArr.SkipWhile((num, index) => num >= index);
            Console.WriteLine($"5. SkipWhile (num >= index): [{string.Join(", ", skipUntilLessThanPos)}]");

            #endregion

            #region LINQ - Quantifiers
            Console.WriteLine("\n=== LINQ - Quantifiers ===");

            // 2. Return a grouped list of products only for categories that have at least one product that is out of stock.
            var categoriesWithAnyOutOfStock = ProductList.GroupBy(p => p.Category)
                                                         .Where(g => g.Any(p => p.UnitsInStock == 0));
            Console.WriteLine("\n2. Categories with ANY out-of-stock product:");
            foreach (var g in categoriesWithAnyOutOfStock)
                Console.WriteLine($" - {g.Key}");

            // 3. Return a grouped list of products only for categories that have all of their products in stock.
            var categoriesWithAllInStock = ProductList.GroupBy(p => p.Category)
                                                      .Where(g => g.All(p => p.UnitsInStock > 0));
            Console.WriteLine("\n3. Categories with ALL products in stock:");
            foreach (var g in categoriesWithAllInStock)
                Console.WriteLine($" - {g.Key}");

            #endregion
        }
    }
}