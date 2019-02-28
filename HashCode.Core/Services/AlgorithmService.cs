using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using HashCode.Core.Domain;
using HashCode.Core.Interfaces;

namespace HashCode.Core.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        public Result RunCode(Collection fileContents, int codebase)
        {
            switch (codebase)
            {
                case 1:
                    return RunCode1(fileContents);

                case 2:
                    return RunCode2(fileContents);

                case 3:
                    return RunCode3(fileContents);

                case 4:
                    return RunCode4(fileContents);

                case 5:
                    return RunCode5(fileContents);
                default:
                    throw new NotImplementedException();
                    break;

            }
        }

        public Result RunCode1(Collection collection)
        {
            return CommonCode(collection);
        }

        public Result RunCode2(Collection collection)
        {
            return CommonCode(collection);
        }

        public Result RunCode3(Collection collection)
        {
            return CommonCode(collection);
        }

        public Result RunCode4(Collection collection)
        {
            return CommonCode(collection);
        }

        public Result RunCode5(Collection collection)
        {
            return CommonCode(collection);
        }

        public Result CommonCode(Collection collection)
        {
            Result result = new Result
            {
                Slides = new List<Slide>()
            };

            var verticalPhotos = collection.Photos.Where(p => p.IsHorizontal == false).OrderByDescending(x => x.NumberOfTags).ToList();

            var decreasingList = verticalPhotos;

            while (decreasingList.Any())
            {
                var kak = GetMinorityInCommon(decreasingList);

                result.Slides.Add(kak.Item1);

                decreasingList = kak.Item2;
            }

            var horizontalPhotos = collection.Photos.Where(p => p.IsHorizontal).ToList();

            var amountOfSlides = horizontalPhotos.Count() + (verticalPhotos.Count() / 2);

            //for (var i = 0; i < verticalPhotos.Count(); i = i + 2)
            //{
            //    var j = i + 1;
            //    var slide = new Slide
            //    {
            //        Photos = new List<Photo>
            //        {
            //            verticalPhotos[i],
            //            verticalPhotos[j]
            //        }
            //    };
            //    result.Slides.Add(slide);
            //}

            foreach (var photo in horizontalPhotos)
            {
                var slide = new Slide
                {
                    Photos = new List<Photo>
                    {
                        photo
                    }
                };

                result.Slides.Add(slide);
            }

            return result;
        }

        private Tuple<Slide, List<Photo>> GetMinorityInCommon(List<Photo> photos)
        {
            var toCheck = photos.FirstOrDefault();
            var orderedList = new List<Photo>();
            if (toCheck != null)
            {
                orderedList = photos.OrderBy(x => x.Tags.Except(toCheck.Tags)).ToList();
            }

            var slide = new Slide
            {
                Photos = new List<Photo>
                {
                    toCheck,
                    orderedList[0]
                }
            };

            photos.Remove(toCheck);
            photos.Remove(orderedList[0]);

            return new Tuple<Slide, List<Photo>>(slide, photos);
        }
    }
}
