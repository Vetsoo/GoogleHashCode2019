using System;
using System.Collections.Generic;
using System.Linq;
using HashCode.Core.Domain;
using HashCode.Core.Interfaces;

namespace HashCode.Core.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        public Result AssignRides(FileContents fileContents)
        {
            var result = new Result
            {
                Vehicles = new List<Vehicle>()
            };

            for (var vehicle = 1; vehicle <= fileContents.NumberOfVehicles; vehicle++)
            {
                result.Vehicles.Add(new Vehicle());
            }

            for (var steps = 0; steps < fileContents.NumberOfStepsInSimulation; steps++)
            {
                foreach (var vehicle in result.Vehicles.Where(x => x.StepsToGo == steps))
                {
                    if (vehicle.StepsToGo == steps && steps != 0)
                    {
                        vehicle.CurrentPosition = vehicle.AssignedRides.LastOrDefault().FinishIntersection;
                    }

                    foreach (var ride in fileContents.Rides.Where(x => !x.Executed))
                    {
                        var rideDistance = Math.Abs(ride.FinishIntersection.Item1 - ride.StartIntersection.Item1) +
                                           Math.Abs(ride.FinishIntersection.Item2 - ride.StartIntersection.Item2);

                        if (ride.EarliestStart > steps)
                        if (vehicle.StepsToGo == steps)
                        {
                            var distanceToStartPostion =
                                Math.Abs((vehicle.CurrentPosition.Item1 - ride.StartIntersection.Item1) +
                                         (vehicle.CurrentPosition.Item2 - ride.StartIntersection.Item2));

                            var distance = distanceToStartPostion + rideDistance;

                            ride.Executed = true;
                            vehicle.StepsToGo += distance;
                            vehicle.AssignedRides.Add(ride);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
