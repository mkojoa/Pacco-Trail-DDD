using System;

namespace Pacco.Services.Availability.Core.ValueObjects
{
    public struct Reservation : IEquatable<Reservation>
    {
        public DateTime DateTime { get; }
        public int Priority { get; }
        
        public Reservation(DateTime dateTime, int priority)
            => (DateTime, Priority) = (dateTime, priority);

        public bool Equals(Reservation other)
            =>  DateTime.Equals(other.DateTime) && Priority == other.Priority;
        

        public override bool Equals(object obj)
            => obj is Reservation other && Equals(other);
        

        public override int GetHashCode()
        {
            unchecked
            {
                return(DateTime.GetHashCode() * 397) ^ Priority;
            }
        }
    }
}