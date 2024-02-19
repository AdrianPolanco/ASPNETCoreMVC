using coreWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreWebApplication.Controllers
{
    //[Route("Customers")]
    public class CustomerController : Controller
    {
        public static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 101, Name = "Adrian", Amount = 50000, Aprobado = true},
            new Customer { Id = 102, Name = "Claudin", Amount = 4521, Aprobado = false}
        };

        public static List<object> estados = new List<object>();

        //[Route("{id:int}")]
        public IActionResult Index([FromRoute] int id)
        {
            //We can also use ViewData["Key"] in order to get the same functionality
            ViewBag.Id = id;
            //Adding my data to my View
            ViewBag.Message = "Message from the ViewBag";
            ViewBag.CustomersQuantity = customers.Count;
            ViewBag.Customers = customers;
            ViewData["Estados"] = estados;
            TempData["Token"] = "Anyhing";
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult NoLayout()
        {
            return View();
        }

        public IActionResult Temp()
        {
            TempData["Message"] = "Message from the ViewBag";
            TempData["CustomersQuantity"] = customers.Count;
            ViewData["Customers"] = customers;
            if (TempData["Token"] == null) return RedirectToAction("Index");

            return View();
        }

        public IActionResult Login()
        {
            //Setting a session cookie
            HttpContext.Session.SetString("Username", "Adrian");
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            //Getting a session cookie
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        public IActionResult LogOut()
        {
            //Removing a session cookie
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index");
        }

        public IActionResult Query()
        {
            string name = "King Adrian";
            //Checking if the query parameters are null or empty, if they arent, then we are asigning to name variable the value that has the query parameter called name
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["name"])) name = HttpContext.Request.Query["name"];
            //Creating a ViewBag variable with the value of the query parameter so we can use it inside the view
            ViewBag.Name = name;    
            return View();
        }
    }
}
