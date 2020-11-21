using System;
using System.Collections.Generic;

namespace DataDefinition.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public int SomeCount { get; set; }

        public List<DependentEntity> DependentEntities { get; set; }
    }
}