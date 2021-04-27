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
        
        // public IActionResult Index() => View(_northwindContext.CartItems);
        public String Index() {
            var deta = _northwindContext.CartItems.Include("Products");
            return "tets";
        }
        
    }
}