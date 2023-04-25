using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    internal class Produce
    {
        /// <summary>
        /// LuggageBelt holds all unsorted pieces of luggage
        /// </summary>
      
        public static Queue<Luggage> luggageBelt = new();
        readonly static Random random = new();


        public static void ProduceLuggage()
        {
            // Terminal Locations
            // Copenhagen airport = CPH -- Denmark
            // Bangkok airport = BKK -- Thailand
            // Sydney airport = SYD -- Australia


            // Randomly generates luggage, the destination is depending on the random number
            // All produced/checked in luggage gets enqueued to the luggageBelt
            while (true)
            {
                try
                {
                    Monitor.Enter(luggageBelt);
                    int randomNumber = random.Next(1, 4);

                    switch (randomNumber)
                    {
                        case 1:
                            luggageBelt.Enqueue(new Luggage("CPH"));
                            break;
                        case 2:
                            luggageBelt.Enqueue(new Luggage("BKK"));
                            break;
                        case 3:
                            luggageBelt.Enqueue(new Luggage("SYD"));
                            break;
                        default:
                            luggageBelt.Enqueue(new Luggage("Lost Luggage"));
                            break;
                    }

                    // Pulses to the waiting thread
                    Monitor.Pulse(luggageBelt);
                }
                finally
                {
                    // Adds an status message to the list
                    Program.statusMessageQueue.Add($"Check in: " +
                        $"\nDestination:{luggageBelt.Peek().Destination}" +
                        $"\nNumber: {luggageBelt.Peek().LuggageNumber}" +
                        $"\nDate stamp: {luggageBelt.Peek().DateStamp}");

                    Monitor.Exit(luggageBelt);
                    Thread.Sleep(200);
                }
            }
        }
    }
}
