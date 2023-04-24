using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagagesorteringssystem
{

    internal class Plane
    {

        private string destination;

        public Stack<Luggage> luggageInPlane = new();
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public Plane(string destination)
        {
            this.destination = destination;
        }
    }
}
