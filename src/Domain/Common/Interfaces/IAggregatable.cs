using System;

namespace Chatty.Domain.Common.Interfaces
{
    public interface IAggregatable
    {
        public DateTime Occurrence { get; }
    }
}
