using System;
using ElevatorStrategy;
using ElevatorStrategy.Enumerators;

int startingFloor = 2;

int[] hallButtons = { 1, 3, 6, 10 };
int[] cabinButtons = { 7, 9, 8 };

int secondsPerFloor = 1;
int secondsPerStop = 2;

var elevator = new Elevator(startingFloor, hallButtons, cabinButtons);

InsidePriorityStrategy insidePriorityNoSortStrategy = new InsidePriorityStrategy(secondsPerFloor, secondsPerStop, "Inside Priority Floor Planning Strategy No Sort");
InsidePriorityStrategy insidePriorityStrategy = new InsidePriorityStrategy(secondsPerFloor, secondsPerStop, "Inside Priority Floor Planning Strategy");
FloorPlanningStrategy floorPlanningStrategy = new FloorPlanningStrategy(secondsPerFloor, secondsPerStop, "Floor Planning Strategy");
FloorPlanningStrategy floorPlanningStrategy2 = new FloorPlanningStrategy(secondsPerFloor, secondsPerStop, "Floor Planning Strategy2");


FloorPlanResult resultInsideNoSort = insidePriorityNoSortStrategy.CreatePlan(elevator.CurrentFloor, Direction.Up, elevator.HallButtons, elevator.CabinButtons, false);
FloorPlanResult resultInside = insidePriorityStrategy.CreatePlan(elevator.CurrentFloor, Direction.Up, elevator.HallButtons, elevator.CabinButtons, true);
FloorPlanResult result = floorPlanningStrategy.CreatePlan(elevator.CurrentFloor, Direction.Up, elevator.HallButtons, elevator.CabinButtons);
FloorPlanResult result2 = floorPlanningStrategy2.CreatePlan(elevator.CurrentFloor, Direction.Down, elevator.HallButtons, elevator.CabinButtons);

elevator.ReceivePlan(resultInsideNoSort, insidePriorityNoSortStrategy.StrategyName);
elevator.ReceivePlan(resultInside, insidePriorityStrategy.StrategyName);
elevator.ReceivePlan(result, floorPlanningStrategy.StrategyName);
elevator.ReceivePlan(result2, floorPlanningStrategy2.StrategyName);

