using System;

namespace RezRouting.Demos.MvcWalkthrough2.DataAccess
{
    public class Review
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public string UserName { get; set; }

        public int Score { get; set; }

        public string Comments { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}