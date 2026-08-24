using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    public class Elevator
    {
        private readonly List<ElevatorRequest> requests = [];
        public int CurrentFloor { get; private set; }
        public int CurrentTime { get; private set; }
        public Direction Direction { get; private set; } = Direction.Idle;
        private const int SecondsPerFloor = 2;
        private const int DoorTimeSeconds = 3;

    }
}
