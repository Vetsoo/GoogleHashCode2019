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
                Console.WriteLine("Would you like to run all files, or a single file? (A/S)");
                var allOrSingle = Console.ReadLine();

                switch (allOrSingle)
                {
                    case "A":
                        Program.RunAllFiles();
                        break;
                    case "S":
                        Console.WriteLine("File path?");
                        var fileName = Console.ReadLine();
                        Console.WriteLine("Choose which code you would like to run: (1 - 5)");
                        var codebasenr = Console.ReadLine();
                        Program.RunSingleFile(fileName, int.Parse(codebasenr));
                        break;
                    default:
                        throw new NotImplementedException();
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

        private static void RunAllFiles()
        {
            var fileService = new FileService();
            var inputFiles = fileService.GetInputFiles();
            inputFiles.Sort();

            for (int i = 0; i < inputFiles.Count; i++)
            {
                Program.RunSingleFile(inputFiles[i], i);
            }
        }

        private static void RunSingleFile(string fileName, int codeBaseNr)
        {
            var fileService = new FileService();

            var fileContents = fileService.ReadFile(fileName);

            Console.WriteLine($"File {fileName} has been read out.");

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

}