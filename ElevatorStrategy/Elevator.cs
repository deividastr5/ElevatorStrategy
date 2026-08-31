using ElevatorStrategy.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;

namespace ElevatorStrategy
{
    public class Elevator
    {
        public int CurrentFloor { get; }
        public int[] HallButtons { get; }
        public int[] CabinButtons { get; }

        public Elevator(int startingFloor, int[] hallButtons, int[] cabinButtons)
        {
            CurrentFloor = startingFloor;
            HallButtons = hallButtons;
            CabinButtons = cabinButtons;
        }
        public void ReceivePlan(FloorPlanResult result, string strategyName)
        {        
            Console.WriteLine(strategyName);
            var table = new ConsoleTable("", "Values");
            table.AddRow("Floor plan", string.Join(" -> ", result.FloorPlan));
            table.AddRow("Full trip time (s)", result.FullTripTime);
            table.AddRow("Total passengers waiting time (s)", result.TotalPassengerWaitingTime);
            table.AddRow("Average passenger waiting time (s)", result.AveragePassengerWaitingTime);
            table.AddRow("Direction changes", result.DirectionChanges);
            table.Write(); 
        }
    }
}
