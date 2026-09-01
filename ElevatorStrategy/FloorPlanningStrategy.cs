using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    public class FloorPlanningStrategy : StrategyBase
    {
        public FloorPlanningStrategy(int secondsPerFloor, int secondsPerStop, string strategyName) : base(secondsPerFloor, secondsPerStop, strategyName) { }
                   
        public FloorPlanResult CreatePlan(int currentFloor, Direction initialDirection, IEnumerable<int> hallButtons, IEnumerable<int> cabinButtons)
        {
            int[] totalStops = GetTotalStops(currentFloor, initialDirection, hallButtons, cabinButtons);

            Dictionary<int, int> waitingTimes = new Dictionary<int, int>();

            var (_, elapsedTime, directionChanges, _) =
             ProcessStops(totalStops, currentFloor, 0, null, waitingTimes);
           
            int totalWaitingTime = waitingTimes.Values.Sum();

            FloorPlanResult floorPlanResult = new FloorPlanResult
            {
                FloorPlan = totalStops,
                FullTripTime = elapsedTime,
                TotalPassengerWaitingTime = totalWaitingTime,
                AveragePassengerWaitingTime = (double)totalWaitingTime / waitingTimes.Count,
                DirectionChanges = directionChanges,
            };

            return floorPlanResult;
        }
        private int[] GetTotalStops(int currentFloor, Direction initialDirection, IEnumerable<int> hallButtons, IEnumerable<int> cabinButtons)
        {
            int[] requestedFloors = hallButtons.Concat(cabinButtons).Where(floor => floor != currentFloor).Distinct().ToArray();

            int[] totalStops = SortStops(requestedFloors, currentFloor, initialDirection);
          
            return totalStops;
        }
    }
}
