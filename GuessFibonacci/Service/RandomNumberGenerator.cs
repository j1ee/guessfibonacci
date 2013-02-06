using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessFibonacci.Service
{
    public class RandomNumberGenerator
    {
        private int min;
        private int max;
        private Random random;

        public RandomNumberGenerator(Random random, int min, int max)
        {
            this.random = random;
            this.min = min;
            this.max = max;
        }

        public virtual int Generate()
        {
            return random.Next(min, max);
        }

    }
}