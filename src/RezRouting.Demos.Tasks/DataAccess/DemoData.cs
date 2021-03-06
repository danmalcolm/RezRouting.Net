﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting.Demos.Tasks.DataAccess
{
    public class DemoData
    {
        static DemoData()
        {
            Manufacturers = Enumerable.Range(1, 5)
                .Select(x => new Manufacturer { Id = x, Name = "Manufacturer " + x })
                .ToList();

            Products = Enumerable.Range(1, 10)
                .Select(x =>
                {
                    var date = DateTime.Now.AddDays(-x);
                    return new Product
                                 {
                                     Id = x,
                                     Name = "Product " + x,
                                     Manufacturer = Manufacturers.First(),
                                     CreatedOn = date,
                                     ModifiedOn = date,
                                     IsActive = true
                                 };
                })
                .ToList();

            Users = new[] { "Bob", "Mary", "Jane" }
                .Select(x => new User { UserName = x, Password = "123456" })
                .ToList();
        }

        public static List<Manufacturer> Manufacturers { get; set; }

        public static List<Product> Products { get; set; }

        public static List<User> Users { get; set; }

    }
}