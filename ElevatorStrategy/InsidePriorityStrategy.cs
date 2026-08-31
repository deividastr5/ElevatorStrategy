using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    internal class InsidePriorityStrategy : StrategyBase
    {
        public InsidePriorityStrategy(int secondsPerFloor, int secondsPerStop) : base(secondsPerFloor, secondsPerStop) { }

        public FloorPlanResult CreatePlan(int currentFloor, Direction initialDirection, IEnumerable<int> hallButtons, IEnumerable<int> cabinButtons)
        {
            Dictionary<int, int> waitingTimes = new Dictionary<int, int>();

            int[] totalStopsForInsideFloors = GetTotalStops(currentFloor, initialDirection, cabinButtons);

            var (previousFloor, elapsedTime, insideDirectionChanges, previousDirection) =
                ProcessStops(totalStopsForInsideFloors, currentFloor, 0, null, waitingTimes);

            Direction nextDirection = GetNextDirection(hallButtons, previousFloor);

            int[] totalStopsForOutsideFloors = GetTotalStops(previousFloor, nextDirection, hallButtons);

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
        private int[] GetTotalStops(int currentFloor, Direction direction, IEnumerable<int> buttons)
        {
            int[] requestedFloors = buttons.Where(floor => floor != currentFloor).Distinct().ToArray();

            int[] totalStops = SortStops(requestedFloors, currentFloor, direction);

            return totalStops;
        }
        private Direction GetNextDirection(IEnumerable<int> buttons, int previousFloor)
        {
            int nearestFloor = buttons.OrderBy(floor => Math.Abs(floor - previousFloor)).ThenByDescending(floor => floor).First();

            Direction nextDirection = nearestFloor > previousFloor ? Direction.Up : Direction.Down;

            return nextDirection;
        }
        //private (int previousFloor, int elapsedTime, int directionChanges, Direction? previousDirection) ProcessStops(
        //    int[] stops, int startFloor, int elapsedTime, Direction? incomingDirection, Dictionary<int, int> waitingTimes)
        //{
        //    int previousFloor = startFloor;
        //    Direction? previousDirection = incomingDirection;
        //    int directionChanges = 0;

        //    foreach (int nextFloor in stops)
        //    {
        //        Direction movementDirection = nextFloor > previousFloor ? Direction.Up : Direction.Down;

        //        if (previousDirection.HasValue && previousDirection.Value != movementDirection)
        //            directionChanges++;

        //        int travelledFloors = Math.Abs(nextFloor - previousFloor);

        //        elapsedTime += travelledFloors * SecondsPerFloor + SecondsPerStop;

        //        waitingTimes[nextFloor] = elapsedTime;

        //        previousFloor = nextFloor;
        //        previousDirection = movementDirection;
        //    }
        //    return (previousFloor, elapsedTime, directionChanges, previousDirection);
        //}
    }
}
