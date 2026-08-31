using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    public abstract class StrategyBase
    {
        protected int SecondsPerFloor { get; }
        protected int SecondsPerStop { get; }

        protected StrategyBase(int secondsPerFloor, int secondsPerStop)
        {
            SecondsPerFloor = secondsPerFloor;
            SecondsPerStop = secondsPerStop;
        }
        /// <summary>
        /// Processes the stops for the elevator, calculating the elapsed time, direction changes, and waiting times for each stop.
        /// </summary>
        /// <param name="stops">The floors the elevator will stop at.</param>
        /// <param name="startFloor">The floor the elevator starts on.</param>
        /// <param name="elapsedTime">The initial elapsed time before processing the stops.</param>
        /// <param name="incomingDirection">The initial direction of the elevator.</param>
        /// <param name="waitingTimes">A dictionary to record the waiting times for each floor.</param>
        /// <returns>A tuple containing the previous floor, elapsed time, direction changes, and the previous direction.</returns>
        protected (int previousFloor, int elapsedTime, int directionChanges, Direction? previousDirection) ProcessStops(
          int[] stops, int startFloor, int elapsedTime, Direction? incomingDirection, Dictionary<int, int> waitingTimes)
        {
            int previousFloor = startFloor;
            Direction? previousDirection = incomingDirection;
            int directionChanges = 0;

            foreach (int nextFloor in stops)
            {
                Direction movementDirection = nextFloor > previousFloor ? Direction.Up : Direction.Down;

                if (previousDirection.HasValue && previousDirection.Value != movementDirection)
                    directionChanges++;

                int travelledFloors = Math.Abs(nextFloor - previousFloor);

                elapsedTime += travelledFloors * SecondsPerFloor + SecondsPerStop;

                waitingTimes[nextFloor] = elapsedTime;

                previousFloor = nextFloor;
                previousDirection = movementDirection;
            }
            return (previousFloor, elapsedTime, directionChanges, previousDirection);
        }
        /// <summary>
        /// Sorts the floors based on the current floor and direction.
        /// </summary>
        /// <param name="floors">The floors to sort.</param>
        /// <param name="currentFloor">The current floor of the elevator.</param>
        /// <param name="direction">The direction of the elevator.</param>
        /// <returns>An array of sorted floors.</returns>
        protected int[] SortStops(int[] floors, int currentFloor, Direction direction)
        {
            int[] above = floors.Where(f => f > currentFloor).OrderBy(f => f).ToArray();
            int[] below = floors.Where(f => f < currentFloor).OrderByDescending(f => f).ToArray();

            int[] totalStops = direction == Direction.Up
              ? above.Concat(below).ToArray()
              : below.Concat(above).ToArray();

            return totalStops;
        }
    }
}
