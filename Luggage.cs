using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    // Class to represent a piece of luggage or an suitcase
    internal class Luggage
    {

        // Fields
        private DateTime dateStamp;
        private string _destination;
        private int luggageNumber;

        readonly Random rnd = new();

        // Properties
        public int LuggageNumber
        {
            get { return luggageNumber; }
            set { luggageNumber = value; }
        }
        public DateTime DateStamp
        {
            get { return dateStamp; }
            set { dateStamp = value; }
        }
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        // Constructor -
        // Sets the dateStamp to the current date & time
        // randomly generates luggageNumber from 1000 to 9999
        public Luggage(string destination)
        {

            this._destination = destination;
            this.dateStamp = DateTime.Now;
            this.luggageNumber = rnd.Next(1000, 10000);
        }

    }
}
