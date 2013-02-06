using GuessFibonacci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessFibonacci.Service
{
    public class GuessService
    {
        private FibonacciCalculator fibonacciCalculator;

        public GuessService(FibonacciCalculator fibonacciCalculator)
        {
            this.fibonacciCalculator = fibonacciCalculator;
        }

        public virtual GuessResult SubmitGuess(int term, int guess)
        {
            var correctValue = fibonacciCalculator.Calculate(term);
            return new GuessResult { term = term, guess = guess, correctResult = correctValue };
        }
    }
}