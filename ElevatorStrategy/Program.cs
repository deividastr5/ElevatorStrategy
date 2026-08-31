using System;
using ElevatorStrategy;
using ElevatorStrategy.Enumerators;

int startingFloor = 2;

int[] hallButtons = { 1, 3, 6, 10 };
int[] cabinButtons = { 7, 9, 8 };

int secondsPerFloor = 1;
int secondsPerStop = 2;

var elevator = new Elevator(startingFloor, hallButtons, cabinButtons);

FloorPlanningStrategy floorPlanningStrategy = new FloorPlanningStrategy(secondsPerFloor, secondsPerStop);
InsidePriorityStrategy insidePriorityStrategy = new InsidePriorityStrategy(secondsPerFloor, secondsPerStop);

FloorPlanResult result = floorPlanningStrategy.CreatePlan(elevator.CurrentFloor, Direction.Up, elevator.HallButtons, elevator.CabinButtons);
FloorPlanResult result2 = insidePriorityStrategy.CreatePlan(elevator.CurrentFloor, Direction.Up, elevator.HallButtons, elevator.CabinButtons);

elevator.ReceivePlan(result);
elevator.ReceivePlan(result2);


