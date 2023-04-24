using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    internal class Terminal
    {
        public static void LuggageDenmark()
        {
            bool isFlying;
            Plane plane = new("CPH");
            while (true)
            {
                isFlying = false;
                try
                {

                    Monitor.Enter(Program.terminalDenmark);
                    if (Program.terminalDenmark.Count >= 5)
                    {
                        int bagageAmount = Program.terminalDenmark.Count;
                        lock (Program.statusMessageQueue)
                        {
                            Program.statusMessageQueue.Add("Plane to Copenhagen is at max capacity - departure soon");
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(Program.terminalDenmark.Dequeue());
                                Thread.Sleep(50);

                            }
                            isFlying = true;
                            PlaneDeparture(plane);
                        }
                    }
                    Debug.WriteLine($"Denmark luggage: {Program.terminalDenmark.Count}");
                }
                finally
                {
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
        public static void LuggageThailand()
        {
            bool isFlying;
            Plane plane = new("BKK");
            while (true)
            {
                isFlying = false;
                try
                {

                    Monitor.Enter(Program.terminalThailand);
                    if (Program.terminalThailand.Count >= 5)
                    {
                        int bagageAmount = Program.terminalThailand.Count;

                        lock (Program.statusMessageQueue)
                        {
                            Program.statusMessageQueue.Add("Plane to Bangkok is at max capacity - departure soon ");
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(Program.terminalThailand.Dequeue());
                                Thread.Sleep(50);

                            }
                            isFlying = true;
                            PlaneDeparture(plane);
                        }
                    }
                    Debug.WriteLine($"Thailand luggage {Program.terminalThailand.Count}");

                }
                finally
                {
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
        public static void LuggageAustralia()
        {
            bool isFlying;
            Plane plane = new("SYD");
            while (true)
            {
                isFlying = false;
                try
                {
                    Monitor.Enter(Program.terminalAustralia);
                    if (Program.terminalAustralia.Count >= 5)
                    {
                        int bagageAmount = Program.terminalAustralia.Count;

                        lock (Program.statusMessageQueue)
                        {
                            Program.statusMessageQueue.Add("Plane to Sydney is at max capacity - departure soon ");
                            for (int i = 0; i < bagageAmount; i++)
                            {
                                plane.luggageInPlane.Push(Program.terminalAustralia.Dequeue());
                                Thread.Sleep(50);
                            }
                            isFlying = true;
                            PlaneDeparture(plane);
                        }
                    }
                    Debug.WriteLine($"Australia luggage {Program.terminalAustralia.Count}");
                }
                finally
                {
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

        public static void PlaneDeparture(Plane plane)
        {
            plane.luggageInPlane.Clear();
        }
    }
}
