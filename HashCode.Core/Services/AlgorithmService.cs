﻿using System;
using System.Collections.Generic;
using System.Linq;
using HashCode.Core.Domain;
using HashCode.Core.Interfaces;

namespace HashCode.Core.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        public Result RunCode(FileContents fileContents, int codebase)
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

        public Result RunCode1(FileContents fileContents)
        {
            Result result = new Result();
            return result;
        }

        public Result RunCode2(FileContents fileContents)
        {
            Result result = new Result();
            return result;
        }

        public Result RunCode3(FileContents fileContents)
        {
            Result result = new Result();
            return result;
        }

        public Result RunCode4(FileContents fileContents)
        {
            Result result = new Result();
            return result;
        }

        public Result RunCode5(FileContents fileContents)
        {
            Result result = new Result();
            return result;
        }

    }
}
