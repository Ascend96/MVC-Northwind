using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
            SaveChanges();
        }
        public void EditCustomer(Customer customer)
        {
            var customerToUpdate = Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            customerToUpdate.Address = customer.Address;
            customerToUpdate.City = customer.City;
            customerToUpdate.Region = customer.Region;
            customerToUpdate.PostalCode = customer.PostalCode;
            customerToUpdate.Country = customer.Country;
            customerToUpdate.Phone = customer.Phone;
            customerToUpdate.Fax = customer.Fax;
            SaveChanges();
        }
        public CartItem AddToCart(CartItemJSON cartItemJSON)
        {
            int CustomerId = Customers.FirstOrDefault(c => c.Email == cartItemJSON.email).CustomerId;
            int ProductId = cartItemJSON.id;
            // check for duplicate cart item
            CartItem cartItem = CartItems.FirstOrDefault(ci => ci.ProductId == ProductId && ci.CustomerId == CustomerId);
            if (cartItem == null)
            {
                // this is a new cart item
                cartItem = new CartItem()
                {
                    CustomerId = CustomerId,
                    ProductId = cartItemJSON.id,
                    Quantity = cartItemJSON.qty
                };
                CartItems.Add(cartItem);
            }
            else
            {
                // for duplicate cart item, simply update the quantity
                cartItem.Quantity += cartItemJSON.qty;
            }

            SaveChanges();
            cartItem.Product = Products.Find(cartItem.ProductId);
            return cartItem;
        }
        
        public void RemoveFromCart(int id){
            var itemToRemove = CartItems.FirstOrDefault(c => c.CartItemId == id);
            CartItems.Remove(itemToRemove);
            SaveChanges();
        }


        // updates stock based on items in cart upon checkout with api controller
            public void UpdateDatabase(string email){
             var query = CartItems.Where(c => c.Customer.Email == email).ToList();
             foreach(CartItem cartitem in query){
                CartItem cartItem = CartItems.FirstOrDefault(ci => ci.Customer.Email == email && ci.ProductId == cartitem.ProductId);
                Product product = Products.FirstOrDefault(p => p.ProductId == cartitem.ProductId);
                int updatedQuantity = (product.UnitsInStock - cartitem.Quantity);
                product.UnitsInStock = (short)updatedQuantity;
                CartItems.Remove(cartitem);
             }
            
            SaveChanges();
            
        }
    }
}