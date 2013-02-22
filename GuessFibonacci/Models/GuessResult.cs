using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessFibonacci.Models
{
    public class GuessResult
    {
        public int term { get; set; }
        public int guess { get; set; }
        public int correctResult { get; set; }

        public virtual int CalculateDifference()
        {
            var difference = correctResult - guess;
            if (difference < 0)
            {
                difference = -1 * difference;
            }
            return difference;
        }

        public virtual bool isCorrect()
        {
            return guess == correctResult;
        }
    }

        
}