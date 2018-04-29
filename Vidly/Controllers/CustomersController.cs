using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            // Defered execution : The database is queried when we iterate over the data
            // Otherwise it would be ... = _context.Customers.ToList()
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            // Include an attribute : eager loading
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();

            return View(customer);
        }

        public IActionResult New()
        {
            // Action returns a view that includes a form
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

            if (customerInDb == null) return NotFound();

            // _context.Customers.Update(customerInDb);
            // TryUpdateModelAsync<Customer>(customerInDb); not to be used, opens security holes since it updates all the object's properties.

            // Other ways to do the below:
            // 1. Use the AutoMapper library.
            // 2. Use an UpdateCustomerDto with specific properties to update.

            customerInDb.Name = customer.Name;
            customerInDb.Birthday = customer.Birthday;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}