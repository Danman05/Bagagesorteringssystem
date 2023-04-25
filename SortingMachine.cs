using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    internal class SortingMachine
    {
        private static Luggage? luggage;
        public static void SortLuggage()
        {
            while (true)
            {
                try
                {

                    // Locks the some shared ressources
                    Monitor.Enter(Produce.luggageBelt);
                    Monitor.Enter(Terminal.terminalDenmark);
                    Monitor.Enter(Terminal.terminalThailand);
                    Monitor.Enter(Terminal.terminalAustralia);

                    // Check if there is less or equal 3 items on the luggageBelt, if so then wait for more items
                    if (Produce.luggageBelt.Count <= 3)
                    {
                        Monitor.Wait(Produce.luggageBelt);
                    }

                    // Dequeues an item from the luggageBelt, so we use sort it to the correct terminal
                    luggage = Produce.luggageBelt.Dequeue();


                    // Checks the destination of the luggage and queues it to the correct terminal

                    if (luggage.Destination == "CPH")
                    {
                        Terminal.terminalDenmark.Enqueue(luggage);
                    }
                    else if (luggage.Destination == "BKK")
                    {
                        Terminal.terminalThailand.Enqueue(luggage);
                    }
                    else if (luggage.Destination == "SYD")
                    {
                        Terminal.terminalAustralia.Enqueue(luggage);
                    }
                }
                finally
                {
                    // Exits out of all the Entered ressources
                    Monitor.Exit(Produce.luggageBelt);
                    Monitor.Exit(Terminal.terminalDenmark);
                    Monitor.Exit(Terminal.terminalThailand);
                    Monitor.Exit(Terminal.terminalAustralia);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
