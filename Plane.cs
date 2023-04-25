using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    // Class to represenet an plane
    internal class Plane
    {
        // Field
        private string destination;


        public Stack<Luggage> luggageInPlane = new();
        

        // Max luggage capacity for each plane
        public const int MaxLuggageDenmark = 20;
        public const int MaxLuggageThailand = 25;
        public const int MaxLuggageAustralia = 30;

        // Properties
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        // Constructor
        // Takes in an string to represent the destination
        public Plane(string destination)
        {
            this.destination = destination;
        }

        // Method
        /// <summary>
        /// Simple method to act like an Take off
        /// </summary>
        public void TakeOff()
        {
            try
            {
                Monitor.Enter(luggageInPlane);
                Debug.WriteLine($"Flying to {destination} with {luggageInPlane.Count} pieces of luggage ");
                luggageInPlane.Clear();
            }
            finally
            {
                Monitor.Exit(luggageInPlane);
            }
        }
    }

}
