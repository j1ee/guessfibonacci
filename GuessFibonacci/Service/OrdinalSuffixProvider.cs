using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessFibonacci.Service
{
    public class OrdinalSuffixProvider
    {
        public virtual String GetSuffix(int number)
        {
            if(number > 3 && number < 21){
                return "th";
            }
            var modTen = number % 10;
            switch (modTen)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}