using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics;
using System.Runtime.Serialization;
using TransactionAssignmentApp.Models;

namespace TransactionAssignmentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AssignmentContext context;

        public HomeController(ILogger<HomeController> logger, AssignmentContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var transaction = context.Transactions.OrderByDescending(x => x.Id).ToList();
            return View(transaction);
        }

        public IActionResult NewTransaction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewTransaction(string type, int amount, string description)
        {

            if (type == "null" || amount == 0 || description == null)
            {
                ViewBag.empty = true;
                return View();
            }

            else
            {
                var lastBal = context.Transactions.OrderByDescending(x => x.Id).FirstOrDefault();
                var newTrans = new Transaction();
                newTrans.Date = DateTime.Now;

                if (type.Equals("Credit"))
                {
                    newTrans.Credit = amount;
                    newTrans.Description = description;
                    newTrans.Balance = lastBal.Balance + amount;

                }
                else
                {
                    newTrans.Debit = amount;
                    newTrans.Description = description;
                    int remBal = lastBal.Balance - amount;
                    if (remBal < 0)
                    {
                        ViewBag.lowBal = true;
                        return View();

                    }
                    else
                    {
                        newTrans.Balance = remBal;
                    }
                }
                context.Transactions.Add(newTrans);
                context.SaveChanges();
                TempData["newRec"] = true;
                return RedirectToAction("Index");
            }
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
