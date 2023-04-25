using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    internal class Terminal
    {

        // TODO: Check if this works
        public static void Test(ref object queue, ref object destinationStr)
        {
            string destination = (string)destinationStr;
            Queue<Luggage> luggageQueue = (Queue<Luggage>)queue;

            bool isFlying;
            Plane plane = new(destination);

            while (true)
            {
                isFlying = false;
                try
                {

                    Monitor.Enter(luggageQueue);
                    // When this terminal has more than 30 pieces of luggages, then the plane is ready to be loaded

                    if (luggageQueue.Count >= 30)
                    {
                        int bagageAmount =  luggageQueue.Count;
                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add($"Plane to {destination} is at max capacity - departure soon");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(luggageQueue.Dequeue());
                                Thread.Sleep(50);

                            }
                            // Plane is ready to take off
                            isFlying = true;
                            plane.TakeOff();
                        }
                    }
                    Debug.WriteLine($"{destination} luggage: {luggageQueue.Count}");
                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time

                    if (isFlying)
                    {
                        Thread.Sleep(500);
                        Monitor.Exit(luggageQueue);
                    }
                    else
                    {
                        Thread.Sleep(100);
                        Monitor.Exit(luggageQueue);
                    }
                }
            }
        }
        /// <summary>
        /// Moves the luggage from the terminal to Denmark into the plane
        /// </summary>
        public static void LuggageDenmark()
        {

            // Boolean to change the Thread.Sleep time depending if its flying or not
            bool isFlying;

            // Plane constructor takes in an string
            Plane plane = new("CPH");
            while (true)
            {
                isFlying = false;
                try
                {

                    Monitor.Enter(Program.terminalDenmark);
                    // When this terminal has more than 30 pieces of luggages, then the plane is ready to be loaded

                    if (Program.terminalDenmark.Count >= 30)
                    {
                        int bagageAmount = Program.terminalDenmark.Count;
                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add("Plane to Copenhagen is at max capacity - departure soon");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(Program.terminalDenmark.Dequeue());
                                Thread.Sleep(50);

                            }
                            // Plane is ready to take off
                            isFlying = true;
                            plane.TakeOff();
                        }
                    }
                    Debug.WriteLine($"Denmark luggage: {Program.terminalDenmark.Count}");
                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time

                    if (isFlying)
                    {
                        Thread.Sleep(500);
                        Monitor.Exit(Program.terminalDenmark);
                    }
                    else
                    {
                        Thread.Sleep(100);
                        Monitor.Exit(Program.terminalDenmark);
                    }
                }
            }
        }
        /// <summary>
        /// Moves the luggage from the terminal to Thailand into the plane
        /// </summary>
        public static void LuggageThailand()
        {
            // Boolean to change the Thread.Sleep time depending if its flying or not
            bool isFlying;

            // Plane constructor takes in an string
            Plane plane = new("BKK");
            while (true)
            {
                isFlying = false;
                try
                {

                    Monitor.Enter(Program.terminalThailand);
                    // When this terminal has more than 40 pieces of luggages, then the plane is ready to be loaded

                    if (Program.terminalThailand.Count >= 40)
                    {
                        int bagageAmount = Program.terminalThailand.Count;

                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add("Plane to Bangkok is at max capacity - departure soon ");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(Program.terminalThailand.Dequeue());
                                Thread.Sleep(50);

                            }
                            // Plane is ready to take off
                            isFlying = true;
                            plane.TakeOff();
                        }
                    }
                    Debug.WriteLine($"Thailand luggage {Program.terminalThailand.Count}");

                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time
                    if (isFlying)
                    {
                        Thread.Sleep(700);
                        Monitor.Exit(Program.terminalThailand);
                    }
                    else
                    {
                        Thread.Sleep(100);
                        Monitor.Exit(Program.terminalThailand);
                    }
                }
            }
        }

        /// <summary>
        /// Moves the luggage from the terminal to Australia into the plane
        /// </summary>
        public static void LuggageAustralia()
        {
            // Boolean to change the Thread.Sleep time depending if its flying or not
            bool isFlying;

            // Plane constructor takes in an string

            Plane plane = new("SYD");
            while (true)
            {
                isFlying = false;
                try
                {
                    Monitor.Enter(Program.terminalAustralia);

                    // When this terminal has more than 50 pieces of luggages, then the plane is ready to be loaded
                    if (Program.terminalAustralia.Count >= 50)
                    {
                        int bagageAmount = Program.terminalAustralia.Count;

                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add("Plane to Sydney is at max capacity - departure soon ");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack,
                            for (int i = 0; i < bagageAmount; i++)
                            {

                                plane.luggageInPlane.Push(Program.terminalAustralia.Dequeue());
                                Thread.Sleep(50);
                            }
                            // Plane is ready to take off
                            isFlying = true;
                            plane.TakeOff();
                        }
                    }
                    Debug.WriteLine($"Australia luggage {Program.terminalAustralia.Count}");
                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time
                    if (isFlying)
                    {
                        Thread.Sleep(1000);
                        Monitor.Exit(Program.terminalAustralia);
                    }
                    else
                    {
                        Thread.Sleep(100);
                        Monitor.Exit(Program.terminalAustralia);
                    }
                }
            }
        }
    }
}
