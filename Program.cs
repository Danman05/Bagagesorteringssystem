using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;

namespace Bagagesorteringssystem
{
    internal class Program
    {
        // Static queues 
        public static Queue<Luggage> luggageBelt = new();
        public static Queue<Luggage> terminalDenmark = new();
        public static Queue<Luggage> terminalThailand = new();
        public static Queue<Luggage> terminalAustralia = new();

        // Public static List<string> for the other classes to add messages to the statusMessage
        public static List<string> statusMessageQueue = new();

        static void Main()
        {

            // Initializing worker threads to perform different tasks
            Thread produceLuggage = new(Produce.ProduceLuggage);
            Thread sortLuggage = new(SortingMachine.SortLuggage);
            Thread terminalThreadDenmark = new(Terminal.LuggageDenmark);
            Thread terminalThreadThailand = new(Terminal.LuggageThailand);
            Thread terminalThreadAustralia = new(Terminal.LuggageAustralia);

            // Starting all the threads
            produceLuggage.Start();
            sortLuggage.Start();
            terminalThreadDenmark.Start();
            terminalThreadThailand.Start();
            terminalThreadAustralia.Start();

            // GUI
            // Running while the produceLuggage property isAlive is true
            while (produceLuggage.IsAlive)
            {
                try
                {
                    // Enters the shared ressource using the Monitor class to lock it
                    // Safely prints out the luggage capacity for the terminals 
                    Monitor.Enter(terminalDenmark);
                    Monitor.Enter(terminalThailand);
                    Monitor.Enter(terminalAustralia);
                    Console.Clear();
                    Console.WriteLine($"[Terminal to Copenhagen] Luggage: {terminalDenmark.Count}/30");
                    Console.WriteLine($"[Terminal to Bangkok]    Luggage: {terminalThailand.Count}/40");
                    Console.WriteLine($"[Terminal to Sydney]     Luggage: {terminalAustralia.Count}/50");

                    // Prints out an status message if an plane is taking of
                    // Using lock to lock the message
                    if (statusMessageQueue.Count != 0)
                    {
                        lock (statusMessageQueue)
                        {
                            statusMessageQueue.Sort();
                            for (int i = 0; i < statusMessageQueue.Count; i++)
                            {
                                Console.WriteLine($"\n[Messesage] {statusMessageQueue[i]}");

                            }
                            statusMessageQueue.Clear();
                        }
                    }
                }
                finally
                {
                    // Exit out of the shared ressource
                    Monitor.Exit(terminalDenmark);
                    Monitor.Exit(terminalThailand);
                    Monitor.Exit(terminalAustralia);
                    Thread.Sleep(500);
                }
            }

            // Joins all the working threads when they are terminated
            produceLuggage.Join();
            sortLuggage.Join();
            terminalThreadDenmark.Join();
            terminalThreadThailand.Join();
            terminalThreadAustralia.Join();

            Console.Read();

        }
    }
}