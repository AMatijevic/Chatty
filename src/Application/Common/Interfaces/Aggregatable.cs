using Chatty.Domain.Enums;

namespace Chatty.Application.Common.Interfaces
{
    public abstract class Aggregatable
    {
        public AggregationType AggregationType { get; set; } = AggregationType.Minute;
        public int AggregationValue { get; set; } = 1;

        public int GetAggregationValueInMinutes()
        {
            if (AggregationValue == 0)
            {
                AggregationValue = 1;
            }

            return AggregationType switch
            {
                AggregationType.Hour => AggregationValue * 60,
                AggregationType.Day => AggregationValue * 24 * 60,
                AggregationType.Minute => AggregationValue,
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
