using Chatty.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Chatty.Application.Common.Interfaces
{
    public interface IAggregatorService
    {
        Dictionary<DateTime, IEnumerable<TResult>> Aggregate<TResult>(IEnumerable<IAggregatable> items, int minutes = 1)
            where TResult : IAggregatable;
    }
}
