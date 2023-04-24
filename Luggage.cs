using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{
    internal class Luggage
    {
        private DateTime dateStamp;
        private string _destination;
        private int luggageNumber;

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
        Random rnd = new();
        public Luggage(string destination)
        {
            this._destination = destination;
            this.dateStamp = DateTime.Now;
            this.luggageNumber = rnd.Next(1000, 10000);
        }

    }
}
