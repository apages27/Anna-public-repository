using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TipCalculator.Models;

namespace TipCalculator.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //GET: /Home/GetInput
        public ActionResult GetInput()
        {
            CalculatorInput input = new CalculatorInput();
            return View(input);
        }

        [HttpPost]
        public ActionResult GetInput(CalculatorInput input)
        {
            if (ModelState.IsValid)
            {
                CalculatorOutput output = new CalculatorOutput(input);
                return View("Results", output);
            }
            else return View(input);
        }
    }
}
