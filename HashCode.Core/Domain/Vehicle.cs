using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode.Core.Domain
{
    public class Vehicle
    {
        public Vehicle()
        {
            CurrentPosition = new Tuple<int, int>(0, 0);
            StepsToGo = 0;
            AssignedRides = new List<Ride>();
        }

        public Tuple<int, int> CurrentPosition { get; set; }
        public int StepsToGo { get; set; }
        public List<Ride> AssignedRides { get; set; }
    }
}
