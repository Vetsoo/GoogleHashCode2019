using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode.Core.Domain
{
    public class Ride
    {
        public Ride()
        {
            Executed = false;
        }

        public Tuple<int, int> StartIntersection { get; set; }
        public Tuple<int, int> FinishIntersection { get; set; }
        public int EarliestStart { get; set; }
        public int LatestFinish { get; set; }
        public int RideNumber { get; set; }
        public bool Executed { get; set; }
    }
}
