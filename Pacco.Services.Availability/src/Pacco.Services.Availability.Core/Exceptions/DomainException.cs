using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Core.Exceptions
{
    /// <summary>
    /// All domain exception will extend the DomainException.
    /// </summary>
    public abstract class DomainException : Exception
    {
        /// <summary>
        /// Return better code & message for UI guys
        /// </summary>
        public virtual string Code{ get;}

        protected DomainException(string message) : base(message)
        {

        }
    }
}
