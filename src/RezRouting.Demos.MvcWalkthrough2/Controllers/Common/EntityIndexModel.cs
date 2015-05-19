using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Common
{
    public class EntityIndexModel<TEntity, TCriteria>
        where TEntity : Entity
        where TCriteria : EntityCriteria
    {
        public List<TEntity> Items { get; set; }

        public TCriteria Criteria { get; set; }
    }
}