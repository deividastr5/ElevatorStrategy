using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    public class FloorPlanResult
    {
        public IReadOnlyList<int> FloorPlan { get; init; } = Array.Empty<int>();
        public int FullTripTime { get; init; }
        public int TotalPassengerWaitingTime { get; init; }
        public double AveragePassengerWaitingTime { get; init; }
        public int DirectionChanges { get; init; }
        public IReadOnlyDictionary<int, int> WaitingTimeByFloor { get; init; }
            = new Dictionary<int, int>();
    }
}
