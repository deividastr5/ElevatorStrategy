using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    internal class InsidePriorityStrategy : StrategyBase
    {
        public InsidePriorityStrategy(int secondsPerFloor, int secondsPerStop, string strategyName) : base(secondsPerFloor, secondsPerStop, strategyName) {}

        public FloorPlanResult CreatePlan(int currentFloor, Direction initialDirection, IEnumerable<int> hallButtons, IEnumerable<int> cabinButtons, bool Sort)
        {
            Dictionary<int, int> waitingTimes = new Dictionary<int, int>();

            int[] totalStopsForInsideFloors = GetTotalStops(currentFloor, initialDirection, cabinButtons, Sort);

            var (previousFloor, elapsedTime, insideDirectionChanges, previousDirection) =
                ProcessStops(totalStopsForInsideFloors, currentFloor, 0, null, waitingTimes);

            Direction nextDirection = GetNextDirection(hallButtons, previousFloor);

            int[] totalStopsForOutsideFloors = GetTotalStops(previousFloor, nextDirection, hallButtons, Sort);

            int transitionDirectionChange = nextDirection != previousDirection ? 1 : 0;
    
            var (_, fullTripTime, outsideDirectionChanges, _) =
                ProcessStops(totalStopsForOutsideFloors, previousFloor, elapsedTime, null, waitingTimes);

            int totalWaitingTime = waitingTimes.Values.Sum();

            FloorPlanResult floorPlanResult = new FloorPlanResult
            {
                FloorPlan = totalStopsForInsideFloors.Concat(totalStopsForOutsideFloors).ToArray(),
                FullTripTime = fullTripTime,
                TotalPassengerWaitingTime = totalWaitingTime,
                AveragePassengerWaitingTime = (double)totalWaitingTime / waitingTimes.Count,
                DirectionChanges = insideDirectionChanges + outsideDirectionChanges + transitionDirectionChange,
            };

            return floorPlanResult;
        }
        private int[] GetTotalStops(int currentFloor, Direction direction, IEnumerable<int> buttons, bool Sort)
        {
            int[] requestedFloors = buttons.Where(floor => floor != currentFloor).Distinct().ToArray();

            if (!Sort)
                return requestedFloors;
            
            int[] totalStops = SortStops(requestedFloors, currentFloor, direction);

            return totalStops;
        }
        private Direction GetNextDirection(IEnumerable<int> buttons, int previousFloor)
        {
            int nearestFloor = buttons.OrderBy(floor => Math.Abs(floor - previousFloor)).ThenByDescending(floor => floor).First();

            Direction nextDirection = nearestFloor > previousFloor ? Direction.Up : Direction.Down;

            return nextDirection;
        }
    }
}
