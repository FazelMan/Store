using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entity
{
    public abstract class Entity : Entity<int>, IEntity, IEntity<int>
    {
    }

    public interface IEntity : IEntity<int>
    {
    }

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
