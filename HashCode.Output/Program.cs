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
                var j = i + 1;
                Program.RunSingleFile(inputFiles[i], j);
            }
        }

        private static void RunSingleFile(string fileName, int codeBaseNr)
        {
            var fileService = new FileService();

            var fileContents = fileService.ReadFile(fileName);

            Console.WriteLine($"File {fileName} has been read out.");

            Console.WriteLine($"Amount of rows: {fileContents.AmountOfPhotos}");
            Console.WriteLine();

            //TODO Execute algorithm
            var algorithmService = new AlgorithmService();

            var result = algorithmService.RunCode(fileContents, codeBaseNr);

            //TODO Write correct solution to file
            fileService.WriteFile(fileName, result);
        }

    }

}