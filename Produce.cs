﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    internal class Produce
    {
        public static void ProduceLuggage()
        {
            // Terminal Locations
            // Copenhagen airport = CPH -- Denmark
            // Bangkok airport = BKK -- Thailand
            // Sydney airport = SYD -- Australia

            Random random = new();
            int randomNumber;

            // Randomly generates luggage, the destination is depending on the random number
            while (true)
            {
                try
                {
                    Monitor.Enter(Program.luggageBelt);
                    randomNumber = random.Next(1, 4);

                    if (randomNumber == 1)
                        Program.luggageBelt.Enqueue(new Luggage("CPH"));

                    else if (randomNumber == 2)
                        Program.luggageBelt.Enqueue(new Luggage("BKK"));

                    else if (randomNumber == 3)
                        Program.luggageBelt.Enqueue(new Luggage("SYD"));

                    // Pulses to the waiting thread
                    Monitor.Pulse(Program.luggageBelt);
                }
                finally
                {
                    // Adds an status message to the list
                    Program.statusMessageQueue.Add($"Check in: \nDestination:{Program.luggageBelt.Peek().Destination}\nNumber: {Program.luggageBelt.Peek().LuggageNumber}\nDate stamp: {Program.luggageBelt.Peek().DateStamp}");
                    Monitor.Exit(Program.luggageBelt);
                    Thread.Sleep(200);
                }
            }
        }
    }
}
