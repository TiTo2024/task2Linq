using System;
using System.Collections.Generic;

namespace LINQAssignment
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

    public static class ListGenerators
    {
        // Minimal product list used by the LINQ examples in Program.cs
        public static List<Product> ProductList { get; } = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Tea", Category = "Beverages", UnitPrice = 3.50m, UnitsInStock = 10 },
            new Product { ProductID = 2, ProductName = "Coffee", Category = "Beverages", UnitPrice = 5.00m, UnitsInStock = 0 },
            new Product { ProductID = 3, ProductName = "Ham", Category = "Food", UnitPrice = 7.00m, UnitsInStock = 25 },
            new Product { ProductID = 4, ProductName = "Jam", Category = "Food", UnitPrice = 2.50m, UnitsInStock = 0 },
            new Product { ProductID = 5, ProductName = "Bread", Category = "Food", UnitPrice = 1.50m, UnitsInStock = 20 }
        };
    }
}
