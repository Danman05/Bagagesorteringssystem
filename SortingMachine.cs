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
        public static void SortLuggage()
        {
            Luggage luggage;

            while (true)
            {
                try
                {

                    // Locks the some shared ressources
                    Monitor.Enter(Program.luggageBelt);
                    Monitor.Enter(Program.terminalDenmark);
                    Monitor.Enter(Program.terminalThailand);
                    Monitor.Enter(Program.terminalAustralia);

                    // Check if there is less or equal 3 items on the luggageBelt, if so then wait for more items
                    if (Program.luggageBelt.Count <= 3)
                    {
                        Monitor.Wait(Program.luggageBelt);
                    }

                    // Dequeues an item from the luggageBelt, so we use sort it to the correct terminal
                    luggage = Program.luggageBelt.Dequeue();


                    // Checks the destination of the luggage and queues it to the correct terminal

                    if (luggage.Destination == "CPH")
                    {
                        Program.terminalDenmark.Enqueue(luggage);
                    }
                    else if (luggage.Destination == "BKK")
                    {
                        Program.terminalThailand.Enqueue(luggage);
                    }
                    else if (luggage.Destination == "SYD")
                    {
                        Program.terminalAustralia.Enqueue(luggage);
                    }
                }
                finally
                {
                    // Exits out of all the Entered ressources
                    Monitor.Exit(Program.luggageBelt);
                    Monitor.Exit(Program.terminalDenmark);
                    Monitor.Exit(Program.terminalThailand);
                    Monitor.Exit(Program.terminalAustralia);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
