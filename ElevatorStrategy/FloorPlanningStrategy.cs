using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    public class FloorPlanningStrategy
    {
        public int SecondsPerFloor { get; }
        public int SecondsPerStop { get; }  

        public FloorPlanningStrategy(int secondsPerFloor, int secondsPerStop)
        {
            SecondsPerFloor = secondsPerFloor;
            SecondsPerStop = secondsPerStop;
        }
        public FloorPlanResult CreatePlan(int currentFloor, Direction initialDirection, IEnumerable<int> hallButtons, IEnumerable<int> cabinButtons)
        {
            int[] totalStops = GetTotalStops(currentFloor, initialDirection, hallButtons, cabinButtons);

            Dictionary<int, int> waitingTimes = new Dictionary<int, int>();
            int elapsedTime = 0;
            int previousFloor = currentFloor;
            int directionChanges = 0;
            Direction? previousMovementDirection = null;

            foreach (int nextFloor in totalStops)
            {
                Direction movementDirection = nextFloor > previousFloor ? Direction.Up : Direction.Down;

                if (previousMovementDirection.HasValue &&
                    previousMovementDirection.Value != movementDirection)
                {
                    directionChanges++;
                }

                int travelledFloors = Math.Abs(nextFloor - previousFloor);

                elapsedTime += travelledFloors * SecondsPerFloor + SecondsPerStop;
         
                waitingTimes[nextFloor] = elapsedTime;

                previousFloor = nextFloor;
                previousMovementDirection = movementDirection;
            }

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

            int[] sortedFloorsAboveCurrent = requestedFloors.Where(floor => floor > currentFloor).OrderBy(floor => floor).ToArray();

            int[] sortedFloorsBelowCurrent = requestedFloors.Where(floor => floor < currentFloor).OrderByDescending(floor => floor).ToArray();

            int[] totalStops = initialDirection == Direction.Up
                ? sortedFloorsAboveCurrent.Concat(sortedFloorsBelowCurrent).ToArray()
                : sortedFloorsBelowCurrent.Concat(sortedFloorsAboveCurrent).ToArray();

            return totalStops;
        }
    }
}
