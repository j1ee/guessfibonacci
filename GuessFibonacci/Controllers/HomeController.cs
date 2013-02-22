using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuessFibonacci.Service;
using GuessFibonacci.Models;

namespace GuessFibonacci.Controllers
{
    public class HomeController : Controller
    {
        private GuessService guessService;
        private RandomNumberGenerator randomNumberGenerator;
        private OrdinalSuffixProvider ordinalSuffixProvider;

        public HomeController(RandomNumberGenerator randomNumberGenerator, OrdinalSuffixProvider ordinalSuffixProvider, GuessService guessService)
        {
            this.randomNumberGenerator = randomNumberGenerator;
            this.ordinalSuffixProvider = ordinalSuffixProvider;
            this.guessService = guessService;
        }

        public ActionResult Index()
        {
            var term = randomNumberGenerator.Generate();
            Session["term"] = term;
            ViewBag.Term = term;
            ViewBag.Suffix = ordinalSuffixProvider.GetSuffix(term);
            return View();
        }

        public ActionResult Submit(string guess)
        {
            var term = Session["term"];
            if (term == null)
            {
                return RedirectToAction("Index");
            }
            var termInt = (int)term;
            if(IsInvalidGuess(guess))
            {
                ViewBag.Term = term;
                ViewBag.Suffix = ordinalSuffixProvider.GetSuffix(termInt);
                ViewBag.ValidationError = "Your guess must be an integer";
                return View("Index");
            }

            var result = guessService.SubmitGuess(termInt, int.Parse(guess));
            return HandleGuessResult(result);
        }

        private ActionResult HandleGuessResult(GuessResult result)
        {
            if (result.isCorrect())
            {
                ViewBag.Value = result.correctResult;
                ViewBag.Term = result.term;
                ViewBag.Suffix = ordinalSuffixProvider.GetSuffix(result.term);
                return View("Correct");
            }
            ViewBag.Difference = result.CalculateDifference();
            return View("Incorrect");
        }

        private bool IsInvalidGuess(string guess)
        {
            try
            {
                int.Parse(guess);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
     }

}
