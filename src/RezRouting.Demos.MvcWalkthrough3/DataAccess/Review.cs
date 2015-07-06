using System;

namespace RezRouting.Demos.MvcWalkthrough3.DataAccess
{
    public class Review : Entity
    {
        public Product Product { get; set; }

        public string UserName { get; set; }

        public int Score { get; set; }

        public string Comments { get; set; }

        public DateTime ReviewDate { get; set; }

        public ReviewApprovalStatus ApprovalStatus { get; set; }

        public string ApprovalComments { get; set; }
    }
}