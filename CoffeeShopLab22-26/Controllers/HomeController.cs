using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeShopLab22_26.Models;

namespace CoffeeShopLab22_26.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Menu()
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
            List<Items> items = ORM.Items.ToList();
            ViewBag.Items = items;

            return View();
        }

        public ActionResult Add(int id)
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of books
                List<Items> cart = new List<Items>();
                //add this book to it
                cart.Add((from b in db.Items
                          where b.ProductID == id
                          select b).Single());
                //add the list to the session
                Session.Add("Cart", cart);

            }
            else
            {
                //if it does exist, get the list
                List<Items> cart = (List<Items>)(Session["Cart"]);
                //add this book to it
                cart.Add((from b in db.Items
                          where b.ProductID == id
                          select b).Single());
            }
            return View();
        }

        public ActionResult ItemListByQuantity(int Quantity)
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();

            //LINQ Query
            List<Items> itemsByQuantity = (from b in db.Items

                                          where b.Quantity == Quantity

                                          select b).ToList();

            ViewBag.Items = itemsByQuantity;

            return View("Menu");

        }
    }
}