using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers
{
    public class CartController : Controller
    {
        
        private NorthwindContext _northwindContext;
        public CartController(NorthwindContext db) => _northwindContext = db;
        
        //public IActionResult Index() {
        public IActionResult Index(int id) {
            //return User.Identity.Name;
            ViewBag.id = id;
            return View(_northwindContext.CartItems.Where(c => c.Customer.Email == User.Identity.Name).Include("Product"));
        }

        // public IActionResult UpdateQty(CartItem cartItem){
        //     _northwindContext.UpdateQty(cartItem);
        //      return View(_northwindContext.CartItems.Where(c => c.Customer.Email == User.Identity.Name).Include("Product"));
        // }
        

        // public IActionResult Index(int id){

        //     ViewBag.id = id;
        //     return View(_northwindContext.Categories.OrderBy(c => c.CategoryName));
        // }
        
        


        
    }
}