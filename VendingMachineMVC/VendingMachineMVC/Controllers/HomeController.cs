using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachineMVC.Models;

namespace VendingMachineMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private List<VendingItem> GetItems()
        {
            return new List<VendingItem>()
            {
                new VendingItem() { Choice = 1, Description = "Snickers", Price = 0.75M},
                new VendingItem() { Choice = 2, Description = "Sun Chips", Price = 1.50M},
                new VendingItem() { Choice = 3, Description = "Twizzlers", Price = 1.00M},
                new VendingItem() { Choice = 4, Description = "Coke", Price = 0.95M},
                new VendingItem() { Choice = 5, Description = "Red Bull", Price = 2.25M}
            };
        }

        public ActionResult Index()
        {
            UserInput input = new UserInput();
            input.VendingItems = GetItems();

            return View(input);
        }

        [HttpPost]
        public ActionResult Index(UserInput input)
        {
            var allItems = GetItems();
            input.VendingItems = allItems;

            if (ModelState.IsValid)
            {
                // lookup item and make sure amount paid is enough
                
               
                var selectedItem = allItems.FirstOrDefault(i => i.Choice == input.UserChoice.Value);

                if (selectedItem == null)
                {
                    // bad data
                    input.ErrorMessage = "Invalid Choice";
                }
                else
                {
                    if (selectedItem.Price <= input.UserPaid)
                    {
                        // they can buy, calculate change and redirect
                        
                        VendingOutput vend = new VendingOutput(input, selectedItem);
                        return View("Result", vend);
                    }  
                  
                    input.ErrorMessage = "You didn't deposit enough money";
                }
            }

            return View(input);
        }
    }
}
