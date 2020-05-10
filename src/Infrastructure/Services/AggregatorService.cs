using Chatty.Application.Common.Interfaces;
using Chatty.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatty.Infrastructure.Services
{
    public class AggregatorService : IAggregatorService
    {
        public Dictionary<DateTime, IEnumerable<TResult>> Aggregate<TResult>(IEnumerable<IAggregatable> items, int minutes = 1)
            where TResult : IAggregatable
        {
            if (items?.Any() != true)
            {
                return new Dictionary<DateTime, IEnumerable<TResult>>();
            }

            return items
                .OrderBy(i => i.Occurrence)
                .GroupBy(x => x.Occurrence.Ticks / TimeSpan.FromMinutes(minutes).Ticks)
                .Select(values =>
                {
                    var border = new DateTime(values.Key * TimeSpan.FromMinutes(minutes).Ticks);
                    return new { border, values };
                }).ToDictionary(t => t.border, v => v.values.Select(item => (TResult)item));
        }
    }
}
