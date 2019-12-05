using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers {
    public class HomeController : Controller {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController (MyContext context) {
            dbContext = context;
        }

        [HttpGet ("")]
        public IActionResult Index () {
            List<Product> AllProducts = dbContext.Product.Take (4).ToList ();

            ViewModel viewM = new ViewModel ();
            viewM.AllProducts = AllProducts;

            List<Customer> AllCustom = dbContext.Customer.Include (c => c.OrdersCustomer).ThenInclude (p => p.Product).Take (4).ToList ();
            List<Customer> AllCust = dbContext.Customer.OrderByDescending (c => c.CreatedAt).Take (3).ToList ();

            viewM.AllCustomers = AllCust;
            ViewBag.AllOrd = AllCustom;
            return View (viewM);
        }

        [HttpGet ("Products")]
        public IActionResult Privacy () {
            List<Product> AllProducts = dbContext.Product.ToList ();

            ViewModel viewM = new ViewModel ();
            viewM.AllProducts = AllProducts;
            return View (viewM);
        }

        [HttpPost ("AddProduct")]
        public IActionResult AddProduct (ViewModel NewPro) {
            List<Product> AllProducts = dbContext.Product.ToList ();
            Product sub = NewPro.NewProduct;
            dbContext.Product.Add (sub);
            if (ModelState.IsValid) {

                dbContext.SaveChanges ();
                // ViewModel viewM = new ViewModel ();
                // viewM.AllProducs = AllProduct;
                return RedirectToAction ("Privacy");
            }
            return View ("Privacy");
        }

        // Show All Customers & Add customer form
        [HttpGet ("Customer")]
        public IActionResult Customer () {
            List<Customer> AllCust = dbContext.Customer.ToList ();
            ViewModel viewM = new ViewModel ();
            viewM.AllCustomers = AllCust;

            return View (viewM);
        }

        //Add Customer to the data base
        [HttpPost ("AddCustomer")]
        public IActionResult AddCustomer (ViewModel newCust) {
            List<Customer> AllCust = dbContext.Customer.ToList ();
            Customer subm = newCust.NewCustomer;
            foreach (var a in AllCust) {
                if (a.Name == subm.Name) {
                    System.Console.WriteLine ("------------- " + a.Name + " " + subm.Name);

                    return RedirectToAction ("Customer");
                }
            }
            dbContext.Customer.Add (subm);
            if (ModelState.IsValid) {
                dbContext.SaveChanges ();
                return RedirectToAction ("Customer");
            }
            return View ("Customer");
        }

        [HttpGet ("Delete/{CId}")]
        public IActionResult DeleteCustomer (int CId) {
            Customer DeleteC = dbContext.Customer.FirstOrDefault (d => d.CustomerId == CId);
            dbContext.Remove (DeleteC);
            dbContext.SaveChanges ();
            System.Console.WriteLine (" ********* " + DeleteC + " ***********");
            return RedirectToAction ("Customer");
        }

        // Orders Page
        [HttpGet ("Orders")]
        public IActionResult OrdersPage () {
            List<Product> AllProducts = dbContext.Product.ToList ();
            List<Customer> AllCustomers = dbContext.Customer.ToList ();
            // List<Orders> AllOrders = dbContext.Orders.ToList ();
            //Show All Orders
            // List<Customer> AllCustome = dbContext.Customer.Include (o => o.OrdersCustomer).ToList ();

            List<Customer> AllCustom = dbContext.Customer.Include (c => c.OrdersCustomer).ThenInclude (p => p.Product).ToList ();
            ViewBag.AllOrd = AllCustom;
            //----------------
            ViewModel viewM = new ViewModel ();
            viewM.AllProducts = AllProducts;
            viewM.AllCustomers = AllCustomers;
            return View ("Orders", viewM);
        }

        [HttpPost ("AddOrder")]
        public IActionResult AddOrder (ViewModel newO) {
            List<Product> AllProducts = dbContext.Product.ToList ();
            List<Customer> AllCustomers = dbContext.Customer.ToList ();
            // List<Orders> AllOrders = dbContext.Orders.ToList ();
            ViewModel viewM = new ViewModel ();
            viewM.AllProducts = AllProducts;
            viewM.AllCustomers = AllCustomers;

            Orders orderlist = new Orders ();

            orderlist = newO.NewOrder;
            Orders sub = newO.NewOrder;
            Product checkQty = dbContext.Product.FirstOrDefault (p => p.ProductId == sub.ProductId);
            // checkQty

            System.Console.WriteLine ();
            System.Console.WriteLine ("*************" + checkQty.Qty + sub.SoldQty);
            if (ModelState.IsValid) {
                if (sub.SoldQty <= checkQty.Qty) {
                    checkQty.Qty = checkQty.Qty - sub.SoldQty;

                    dbContext.Orders.Add (orderlist);
                } else {
                    return RedirectToAction ("OrdersPage");
                }
                dbContext.SaveChanges ();

                return RedirectToAction ("OrdersPage");
            }
            return View ("Orders", viewM);
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}