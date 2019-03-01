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
        private const string InputFolder = @"C:\Users\mvermeiren\source\repos\HashCode2019\GoogleHashCode2019\Input\";
        private const string OutputFolder = @"C:\Users\mvermeiren\source\repos\HashCode2019\GoogleHashCode2019\Output\";

        public Collection ReadFile(string fileName)
        {
            var path = $"{InputFolder}{fileName}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"{DateTime.Now}: file \"{fileName}\" is not found in folder \"{InputFolder}\"");
            }

            using (var file = File.OpenText(path))
            {
                var fileContents = new Collection();
                var photos = new List<Photo>();
                var line = file.ReadLine();
                var isFirstLine = true;
                var row = -1;
            
                while (line != null)
                {
                    if (isFirstLine)
                    {
                        fileContents.AmountOfPhotos = Convert.ToInt32(line);
                    }
                    else
                    {
                        var photoParameters = line.Split(' ').ToList();
                        var photo = new Photo()
                        {
                            Id = row,
                            IsHorizontal = photoParameters[0] == "H",
                            NumberOfTags = Convert.ToInt32(photoParameters[1]),
                            Tags = new List<string>()
                        };

                        for (var i = 2; i < photoParameters.Count; i++)
                        {
                            photo.Tags.Add(photoParameters[i]);
                        }

                        photos.Add(photo);
                    }

                    line = file.ReadLine();
                    isFirstLine = false;
                    row++;
                }

                fileContents.Photos = photos;

                return fileContents;
            }
        }

        public void WriteFile(string inputFile, Result result)
        {

            var path = $"{OutputFolder}{inputFile.Substring(0, inputFile.IndexOf(".", StringComparison.Ordinal))}.out";

            var sb = new StringBuilder();

            sb.Append($"{result.Slides.Count}\n");

            foreach (var slide in result.Slides)
            {
                if (slide.Photos.Count == 2)
                {
                    sb.Append($"{slide.Photos[0].Id} {slide.Photos[1].Id}\n");
                }
                else
                {
                    sb.Append($"{slide.Photos[0].Id}\n");
                }
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
