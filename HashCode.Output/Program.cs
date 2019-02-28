using HashCode.Infra.InputReader.Services;
using System;
using HashCode.Core.Services;

namespace HashCode.Output
{ 
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileService = new FileService();
                var inputFiles = fileService.GetInputFiles();

                foreach (var inputFile in inputFiles)
                {
                    var fileContents = fileService.ReadFile(inputFile);

                    Console.WriteLine($"File {inputFile} has been read out.");

                    Console.WriteLine($"Amount of rows: {fileContents.Rows}");
                    Console.WriteLine($"Amount of columns: {fileContents.Columns}");
                    Console.WriteLine($"Number of vehicles in fleet: {fileContents.NumberOfVehicles}");
                    Console.WriteLine($"Number of rides: {fileContents.NumberOfRides}");
                    Console.WriteLine($"Bonus for starting on time: {fileContents.PerRideBonusForStartingOnTime}");
                    Console.WriteLine($"Number of steps in simulation: {fileContents.NumberOfStepsInSimulation}");
                    Console.WriteLine();

                    //TODO Execute algorithm
                    var algorithmService = new AlgorithmService();

                    var result = algorithmService.AssignRides(fileContents);

                    //TODO Write correct solution to file
                    fileService.WriteFile(inputFile, result);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}