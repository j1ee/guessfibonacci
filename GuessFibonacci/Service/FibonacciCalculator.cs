using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessFibonacci.Service
{
    public class FibonacciCalculator
    {
        private int max;

        public FibonacciCalculator(int max)
        {
            this.max = max;
        }

        public virtual int Calculate(int term)
        {
            if(term < 1 || term > max){
                throw new Exception("Term must be between 1 and " + max + ".");
            }
            if (term < 3)
            {
                return 1;
            }
            return Calculate(term - 1) + Calculate(term - 2);
        }
    }
}