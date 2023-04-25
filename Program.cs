using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;

namespace Bagagesorteringssystem
{
    internal class Program
    {

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
                    Monitor.Enter(Terminal.terminalDenmark);
                    Monitor.Enter(Terminal.terminalThailand);
                    Monitor.Enter(Terminal.terminalAustralia);
                    Console.Clear();
                    Console.WriteLine($"[Terminal to Copenhagen] Luggage: {Terminal.terminalDenmark.Count}/{Plane.MaxLuggageDenmark}");
                    Console.WriteLine($"[Terminal to Bangkok]    Luggage: {Terminal.terminalThailand.Count}/{Plane.MaxLuggageThailand}");
                    Console.WriteLine($"[Terminal to Sydney]     Luggage: {Terminal.terminalAustralia.Count}/{Plane.MaxLuggageAustralia}");

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
                    Monitor.Exit(Terminal.terminalDenmark);
                    Monitor.Exit(Terminal.terminalThailand);
                    Monitor.Exit(Terminal.terminalAustralia);
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
        public static void WriteLog(string strValue)
        {
            try
            {
                //Logfile
                string path = "Log.txt";
                StreamWriter sw;
                if (!File.Exists(path))
                { sw = File.CreateText(path); }
                else
                { sw = File.AppendText(path); }

                LogWrite(strValue, sw);

                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
        private static void LogWrite(string logMessage, StreamWriter w)
        {
            w.WriteLine("{0}", logMessage);
            w.WriteLine("----------------------------------------");
        }
    }
}