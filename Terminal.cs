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

        /// terminal queues holds all sorted pieces of luggage

        public static Queue<Luggage> terminalDenmark = new();
        public static Queue<Luggage> terminalThailand = new();
        public static Queue<Luggage> terminalAustralia = new();
        

        // Flight time in miliseconds
        private const int FlightTimeDenmark = 500;
        private const int FlightTimeThailand = 750;
        private const int FlightTimeAustralia = 1000;


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

                    Monitor.Enter(terminalDenmark);
                    // When this terminals count is greater than MaxLuggageDenmark, then the plane is ready to be loaded

                    if (terminalDenmark.Count >= Plane.MaxLuggageDenmark)
                    {
                        int bagageAmount = terminalDenmark.Count;
                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add("Plane to Copenhagen is at max capacity - departure soon");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(terminalDenmark.Dequeue());
                            }
                            // Plane is ready to take off
                            isFlying = true;
                            plane.TakeOff();
                        }
                    }
                    Debug.WriteLine($"Denmark luggage: {terminalDenmark.Count}");
                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time

                    if (isFlying)
                        Thread.Sleep(FlightTimeDenmark);
                    else
                        Thread.Sleep(100);

                    Monitor.Exit(terminalDenmark);
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

                    Monitor.Enter(terminalThailand);
                    // When this terminals count is greater than MaxLuggageThailand, then the plane is ready to be loaded

                    if (terminalThailand.Count >= Plane.MaxLuggageThailand)
                    {
                        int bagageAmount = terminalThailand.Count;

                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add("Plane to Bangkok is at max capacity - departure soon ");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(terminalThailand.Dequeue());
                            }
                            // Plane is ready to take off
                            isFlying = true;

                            plane.TakeOff();
                        }
                    }
                    Debug.WriteLine($"Thailand luggage {terminalThailand.Count}");

                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time
                    if (isFlying)
                        Thread.Sleep(FlightTimeThailand);
                    else
                        Thread.Sleep(100);

                    Monitor.Exit(terminalThailand);
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
                    Monitor.Enter(terminalAustralia);

                    // When this terminals count is greater than MaxLuggageAustralia, then the plane is ready to be loaded
                    if (terminalAustralia.Count >= Plane.MaxLuggageAustralia)
                    {
                        int bagageAmount = terminalAustralia.Count;

                        lock (Program.statusMessageQueue)
                        {
                            // Adds an status message
                            Program.statusMessageQueue.Add("Plane to Sydney is at max capacity - departure soon ");

                            // For-loop to dequeue every item in the terminal and Push it to the luggageInPlane stack,
                            for (int i = 0; i < bagageAmount; i++)
                            {

                                plane.luggageInPlane.Push(terminalAustralia.Dequeue());
                            }
                            // Plane is ready to take off
                            isFlying = true;
                            plane.TakeOff();
                            
                        }
                    }
                    Debug.WriteLine($"Australia luggage {terminalAustralia.Count}");
                }
                finally
                {
                    // if isFlying is true, then we Sleep for a little longer, to simulate the flying time
                    if (isFlying)
                        Thread.Sleep(FlightTimeAustralia);
                    else
                        Thread.Sleep(100);

                    Monitor.Exit(terminalAustralia);
                }
            }
        }
    }
}
