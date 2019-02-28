using System.Collections.Generic;
using System.Reflection.Metadata;

namespace HashCode.Core.Domain
{
    public class FileContents
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int NumberOfVehicles { get; set; }
        public int NumberOfRides { get; set; }
        public int PerRideBonusForStartingOnTime { get; set; }
        public int NumberOfStepsInSimulation { get; set; }
        public List<Ride> Rides { get; set; }
    }
}
