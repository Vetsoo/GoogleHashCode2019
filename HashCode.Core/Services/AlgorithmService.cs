using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var totalRowsToDo = verticalPhotos.Count;
            var doneRows = 0;

            for (var i = 0; i < totalRowsToDo; i += 500)
            {
                var decreasingListVert = verticalPhotos.Skip(doneRows).Take(500).ToList();
                doneRows += 500;

                while (decreasingListVert.Any())
                {
                    Debug.WriteLine(doneRows + " for vertical photos");
                    var kak = GetMinorityInCommon(decreasingListVert);

                    kak.Item1.UniqueTags = kak.Item1.Photos[0].Tags.Union(kak.Item1.Photos[1].Tags).ToList();

                    result.Slides.Add(kak.Item1);

                    decreasingListVert = kak.Item2;
                }
            }

            var hPhotos = collection.Photos.Where(p => p.IsHorizontal == true).OrderByDescending(x => x.NumberOfTags).ToList();

            foreach(var hphoto in hPhotos)
            {
                result.Slides.Add(
                    new Slide {
                        Photos = new List<Photo> { hphoto },
                        UniqueTags = hphoto.Tags
                        
                    }
                    );
            }


            result = SortAllSlides(result);        


            

            return result;
        }

        private Result SortAllSlides(Result result)
        {

            result.Slides = result.Slides.OrderByDescending(x => x.UniqueTags.Count()).ToList();

            var totalRowsToDo = result.Slides.Count;
            var doneRows = 0;
            var tempList = new List<Slide>();

            for (var i = 0; i < totalRowsToDo; i += 500)
            {
                var decreasingList = result.Slides.Skip(doneRows).Take(500).ToList();
                doneRows += 500;

                Slide tempSlide1 = null;
                Slide tempSlide2 = null;
                while (decreasingList.Any())
                {
                    if (tempSlide1 == null)
                    {
                        tempSlide1 = decreasingList[0];

                        tempList.Add(tempSlide1);

                        decreasingList.RemoveAt(0);
                        continue;
                    }
                    Debug.WriteLine(doneRows + " for all slides");
                    tempSlide2 = BestMatch(tempSlide1, decreasingList);
                    tempList.Add(tempSlide2);
                    tempSlide1 = tempSlide2;

                    decreasingList.Remove(tempSlide2);
                    tempSlide2 = null;


                }
            }

            result.Slides = tempList;

            return result;
        }

        private Slide BestMatch(Slide tempSlide1, List<Slide> slides)
        {
            var highscore = -1;
            Slide tempResult = null;
            foreach(var slide in slides)
            {
                var scoreSlide1 = 0;
                var scoreSlideMiddle = 0;
                var scoreSlide2 = 0;

                var commonTags = tempSlide1.UniqueTags.Intersect(slide.UniqueTags);
                var leftTags = tempSlide1.UniqueTags.Except(commonTags);
                var rightTags = slide.UniqueTags.Except(commonTags);

                scoreSlide1 = leftTags.Count();
                scoreSlideMiddle = commonTags.Count();
                scoreSlide2 = rightTags.Count();

                var minFirst2 = Math.Min(scoreSlide1, scoreSlideMiddle);
                var temphighscore = Math.Min(minFirst2, scoreSlide2);

                if(temphighscore > highscore)
                {
                    highscore = temphighscore;
                    tempResult = slide;
                }
            }

            return tempResult;
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

            var slide2 = new Slide
            {
                Photos = new List<Photo>
                {
                    foundPic
                }
            };

            var removed = photos.Remove(toCheck);
            var del = photos.Remove(foundPic);

            return new Tuple<Slide, Slide, List<Photo>>(slide1, slide2, photos);
        }
    }
}
