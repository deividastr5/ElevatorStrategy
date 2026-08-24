using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorStrategy
{
    public class ElevatorRequest
    {
        public int Floor { get; }
        public RequestType Type { get; }
        public Direction RequestedDirection { get; }
        public int PickedUpAt { get; set; }
        public int? CompletedAt { get; set; }
    }
}
