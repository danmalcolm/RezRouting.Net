using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting.Demos.MvcWalkthrough2.DataAccess
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
                    Name = "Manufacturer " + manufacturerId,
                    CreatedOn = DateTime.Now.AddDays(-14).AddDays(-manufacturerId),
                    ModifiedOn = DateTime.Now.AddDays(-14).AddDays(-manufacturerId),
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

        public static TEntity Get<TEntity>(int id)
            where TEntity : Entity
        {
            // We're trying to mimic features of an ORM like
            // NHibernate or Entity Framework here.

            Entity entity = null;

            if (typeof (TEntity) == typeof (Product))
                entity = Products.SingleOrDefault(x => x.Id == id);

            if (typeof(TEntity) == typeof(Manufacturer))
                entity = Manufacturers.SingleOrDefault(x => x.Id == id);

            return entity as TEntity;
        }

        public static IQueryable<TEntity> Query<TEntity>()
            where TEntity : Entity
        {
            // We're trying to mimic features of an ORM like
            // NHibernate or Entity Framework here.

            if (typeof(TEntity) == typeof(Product))
                return (IQueryable<TEntity>) Products.AsQueryable();

            if (typeof(TEntity) == typeof(Manufacturer))
                return (IQueryable<TEntity>)Manufacturers.AsQueryable();

            throw new NotSupportedException("Unrecognised entity type");
        }
    }
}