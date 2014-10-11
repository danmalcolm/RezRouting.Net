using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting.Demos.Crud.DataAccess
{
    public class DemoData
    {
        static DemoData()
        {
            Users = new[] { "Bob", "Mary", "Jane", "Jim" }
                .Select(userName => new User { UserName = userName, Password = "123456" })
                .ToList();

            Manufacturers = (from manufacturerId in Enumerable.Range(1, 5)
                select new Manufacturer
                {
                    Id = manufacturerId, 
                    Name = "Manufacturer " + manufacturerId
                })
                .ToList();

            Products = (from productId in Enumerable.Range(1, 10)
                select new Product
                {
                    Id = productId,
                    Name = "Product " + productId,
                    Manufacturer = Manufacturers.First(),
                    CreatedOn = DateTime.Now.AddDays(-14).AddDays(-productId),
                    ModifiedOn = DateTime.Now.AddDays(-14).AddDays(-productId),
                    IsActive = true
                }).ToList();

            Reviews = (from product in Products
                from reviewNumber in Enumerable.Range(1, 10)
                let good = reviewNumber % 2 == 0
                select new Review
                {
                    Id = (product.Id - 1) * 10 + reviewNumber,
                    Product = product,
                    ReviewDate = product.CreatedOn.AddHours(reviewNumber),
                    Comments = (good ? "It's good " : "I don't like it") + TestDataHelper.Lorem,
                    Score = good ? 10 : 2,
                    UserName = Users[(reviewNumber % 4)].UserName
                })
                .ToList();
            
        }

        public static List<Manufacturer> Manufacturers { get; set; }

        public static List<Product> Products { get; set; }

        public static List<Review> Reviews { get; set; }

        public static List<User> Users { get; set; }
    }
}