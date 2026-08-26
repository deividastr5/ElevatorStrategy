using System;
using ElevatorStrategy;
using ElevatorStrategy.Enumerators;

int startingFloor = 2;

int[] hallButtons = { 1, 3, 6, 10 };
int[] cabinButtons = { 7, 9, 8 };

int secondsPerFloor = 1;
int secondsPerStop = 2;

var elevator = new Elevator(startingFloor);

FloorPlanningStrategy floorPlanningStrategy = new FloorPlanningStrategy(secondsPerFloor, secondsPerStop);

FloorPlanResult result = floorPlanningStrategy.CreatePlan(elevator.CurrentFloor, Direction.Up, hallButtons, cabinButtons);

//elevator.ReceivePlan(result);


