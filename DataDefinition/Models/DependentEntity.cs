using System;

namespace DataDefinition.Models
{
    public class DependentEntity
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public int AnotherCount { get; set; }
        
        public Entity Entity { get; set; }
    }
}