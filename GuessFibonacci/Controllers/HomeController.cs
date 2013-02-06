using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuessFibonacci.Service;

namespace GuessFibonacci.Controllers
{
    public class HomeController : Controller
    {
        private RandomNumberGenerator randomNumberGenerator;
        private OrdinalSuffixProvider ordinalSuffixProvider;
        private FibonacciCalculator fibonacciCalculator;

        public HomeController(RandomNumberGenerator randomNumberGenerator, OrdinalSuffixProvider ordinalSuffixProvider, FibonacciCalculator fibonacciCalculator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
            this.ordinalSuffixProvider = ordinalSuffixProvider;
            this.fibonacciCalculator = fibonacciCalculator;
        }

        public ActionResult Index()
        {
            var term = randomNumberGenerator.Generate();
            ViewBag.Term = term;
            ViewBag.Suffix = ordinalSuffixProvider.GetSuffix(term);
            return View();
        }

        public ActionResult Submit(int term, string guess)
        {
            ViewBag.Term = term;
            ViewBag.Suffix = ordinalSuffixProvider.GetSuffix(term);
            int guessInt;
            try
            {
                guessInt = int.Parse(guess);
            }
            catch (Exception)
            {
                ViewBag.ValidationError = "Your guess must be an integer";
                return View("Index");
            }

            var correctValue = fibonacciCalculator.Calculate(term);
            if (correctValue == guessInt)
            {
                ViewBag.Value = correctValue;
                return View("Correct");
            }
            ViewBag.Difference = CalculateDifference(correctValue, guessInt);
            return View("Incorrect");
        }

        private string TermWithSuffix(int term)
        {
            return term + ordinalSuffixProvider.GetSuffix(term);
        }

        private int CalculateDifference(int correct, int guess)
        {
            var difference = correct - guess;
            if (difference < 0)
            {
                difference = -1 * difference;
            }
            return difference;
        }
    }
}
