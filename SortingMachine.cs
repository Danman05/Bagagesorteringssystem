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
                    Monitor.Enter(Program.luggageBelt);
                    Monitor.Enter(Program.terminalDenmark);
                    Monitor.Enter(Program.terminalThailand);
                    Monitor.Enter(Program.terminalAustralia);

                    if (Program.luggageBelt.Count <= 5)
                    {
                        Monitor.Wait(Program.luggageBelt);
                    }

                    luggage = Program.luggageBelt.Dequeue();

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
