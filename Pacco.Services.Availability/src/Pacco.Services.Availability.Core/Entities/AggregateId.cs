using System;
using Pacco.Services.Availability.Core.Exceptions;

namespace Pacco.Services.Availability.Core.Entities
{
    public class AggregateId : IEquatable<AggregateId>
    {
        /// <summary>
        /// Guid value
        /// </summary>
        public Guid Value { get; }

        public AggregateId() : this(Guid.NewGuid())
        {}

        public AggregateId(Guid value)
        {
            // Id shouldnt be empty
            if (Value == Guid.Empty)
            {
                throw new InvalidAggregateIdException(value);
            }

            // set the value
            Value = value;
        }

        public bool Equals(AggregateId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AggregateId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator Guid(AggregateId id) => id.Value;
        
        public static implicit operator AggregateId(Guid id) => new AggregateId(id);

        public override string ToString() => Value.ToString();
    }
}
