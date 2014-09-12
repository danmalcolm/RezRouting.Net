using System;
using System.Collections.Generic;

namespace RezRouting.Demos.Crud.DataAccess
{
    public class DemoData
    {
        static DemoData()
        {
            Products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", AddedDate = DateTime.Now.AddDays(-1) },
                new Product { Id = 2, Name = "Product 2", AddedDate = DateTime.Now.AddDays(-2) },
                new Product { Id = 3, Name = "Product 3", AddedDate = DateTime.Now.AddDays(-3) },
                new Product { Id = 4, Name = "Product 4", AddedDate = DateTime.Now.AddDays(-4) },
                new Product { Id = 5, Name = "Product 5", AddedDate = DateTime.Now.AddDays(-5) },
            };
            Users = new List<User>
            {
                new User { UserName = "Bob", Password = "123456" },
                new User { UserName = "Mary", Password = "123456" },
                new User { UserName = "Jane", Password = "123456" }
            };
        }

        public static List<Product> Products { get; set; }

        public static List<User> Users { get; set; }

    }
}