using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HashCode.Core.Domain;
using HashCode.Core.Interfaces;

namespace HashCode.Infra.InputReader.Services
{
    public class FileService : IFileService
    {
        private const string InputFolder = @"C:\Dev\Personal\GoogleHashCode2018\Input\";
        private const string OutputFolder = @"C:\Dev\Personal\GoogleHashCode2018\Output\";

        public FileContents ReadFile(string fileName)
        {
            var path = $"{InputFolder}{fileName}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"{DateTime.Now}: file \"{fileName}\" is not found in folder \"{InputFolder}\"");
            }

            using (var file = File.OpenText(path))
            {
                var inputVariables = new List<string>();
                var fileContents = new FileContents();
                var rides = new List<Ride>();
                var line = file.ReadLine();
                var isFirstLine = true;
                var row = -1;


                while (line != null)
                {
                    if (isFirstLine)
                    {
                        inputVariables = line.Split(' ').ToList();
                        fileContents.Rows = Convert.ToInt32(inputVariables[0]);
                        fileContents.Columns = Convert.ToInt32(inputVariables[1]);
                        fileContents.NumberOfVehicles = Convert.ToInt32(inputVariables[2]);
                        fileContents.NumberOfRides = Convert.ToInt32(inputVariables[3]);
                        fileContents.PerRideBonusForStartingOnTime = Convert.ToInt32(inputVariables[4]);
                        fileContents.NumberOfStepsInSimulation = Convert.ToInt32(inputVariables[5]);
                    }
                    else
                    {
                        var rideParameters = line.Split(' ').ToList();
                        var ride = new Ride()
                        {
                            StartIntersection = new Tuple<int, int>(Convert.ToInt32(rideParameters[0]), Convert.ToInt32(rideParameters[1])),
                            FinishIntersection = new Tuple<int, int>(Convert.ToInt32(rideParameters[2]), Convert.ToInt32(rideParameters[3])),
                            EarliestStart = Convert.ToInt32(rideParameters[4]),
                            LatestFinish = Convert.ToInt32(rideParameters[5]),
                            RideNumber = row
                        };
                        rides.Add(ride);
                    }

                    line = file.ReadLine();
                    isFirstLine = false;
                    row++;
                }

                fileContents.Rides = rides;

                return fileContents;
            }
        }

        public void WriteFile(string inputFile, Result result)
        {

            var path = $"{OutputFolder}{inputFile.Substring(0, inputFile.IndexOf(".", StringComparison.Ordinal))}.out";

            var counter = 1;

            var sb = new StringBuilder();

            foreach (var vehicle in result.Vehicles)
            {
                sb.Append($"{vehicle.AssignedRides.Count} ");
                foreach (var rides in vehicle.AssignedRides)
                {
                    sb.Append(rides.RideNumber + " ");
                }
                sb.Append("\n");
                counter++;
            }

            File.AppendAllText(path, sb.ToString());
        }

        public List<string> GetInputFiles()
        {
            var inputFiles = Directory.GetFiles(InputFolder).Select(Path.GetFileName).ToList();

            return inputFiles;
        }
    }
}
