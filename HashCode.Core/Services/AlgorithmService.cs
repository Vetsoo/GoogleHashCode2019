using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

            //var verticalPhotos = collection.Photos.Where(p => p.IsHorizontal == false).OrderByDescending(x => x.NumberOfTags).ToList();

            //var decreasingList = verticalPhotos;

            //while (decreasingList.Any())
            //{
            //    var kak = GetMinorityInCommon(decreasingList);

            //    result.Slides.Add(kak.Item1);

            //    decreasingList = kak.Item2;
            //}



            //var amountOfSlides = horizontalPhotos.Count() + (verticalPhotos.Count() / 2);

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

            var stepSize = collection.Photos.Count / 20;
            var count = collection.Photos.Count;

            List<Task<Result>> taskList = new List<Task<Result>>();

            for(int i=0; i<=20; i++)
            {
                taskList.Add(Task<Result>.Factory.StartNew(() => ExecuteCode(collection.Photos.Skip((stepSize * i)).Take(stepSize).ToList())));
            }

            for (int i = 0; i < taskList.Count; i++)
            {
                result.Slides.AddRange(taskList[i].Result.Slides);
            }

            //foreach (var photo in horizontalPhotos)
            //{
            //    var slide = new Slide
            //    {
            //        Photos = new List<Photo>
            //        {
            //            photo
            //        }
            //    };

            //    result.Slides.Add(slide);
            //}

                return result;
        }

        private Result ExecuteCode(List<Photo> photos)
        {
            

            var decreasingList = photos;
            var result = new Result();
            result.Slides = new List<Slide>();

            while (decreasingList.Any())
            {
                var kak = GetHigherAs0(decreasingList);

                if(kak.Item2.Photos[0] != null){
                    if(!result.Slides.Any(s => (s.Photos[0].Id == kak.Item1.Photos[0].Id) && (s.Photos[0].Id == kak.Item2.Photos[0].Id)))
                    {
                        result.Slides.Add(kak.Item1);
                        result.Slides.Add(kak.Item2);
                    }
                 }
                

                decreasingList = kak.Item3;
            }
            
            return result;
        }

        private Tuple<Slide, List<Photo>> GetMinorityInCommon(List<Photo> photos)
        {
            var toCheck = photos.FirstOrDefault();
            var orderedList = new List<Photo>();
            if (toCheck != null)
            {
                var wut = new Dictionary<Photo, int>();
                foreach (var photo in photos)
                {
                    var amount = photo.Tags.Except(toCheck.Tags).Count();
                    wut.Add(photo, amount);
                }

                var order = wut.OrderByDescending(x => x.Value);

                foreach (var pic in order)
                {
                    orderedList.Add(pic.Key);
                }
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

        private Tuple<Slide, Slide, List<Photo>> GetHigherAs0(List<Photo> photos)
        {
            var toCheck = photos.FirstOrDefault();
            Photo foundPic = null;
            if (toCheck != null)
            {
                foreach (var photo in photos)
                {
                    var amount = photo.Tags.Intersect(toCheck.Tags).Count();
                    if (photo.Id != toCheck.Id && amount > 0)
                    {
                        foundPic = photo;
                        break;                        
                    }
                }
            }

            var slide1 = new Slide
            {
                Photos = new List<Photo>
                {
                    toCheck
                }
            };

            if(foundPic != null)
            {

            }

            var slide2 = new Slide
            {
                Photos = new List<Photo>
                {
                    foundPic
                }
            };

            var removed = photos.Remove(toCheck);
            

            return new Tuple<Slide, Slide, List<Photo>>(slide1, slide2, photos);
        }
    }
}
