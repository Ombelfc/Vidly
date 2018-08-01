using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(await _context.Customers.ToListAsync()));
        }

        // GET /api/customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.Where(c => c.Id == id).SingleOrDefaultAsync();

            if(customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCsutomer([FromBody]CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _mapper.Map<Customer>(customerdto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerdto.Id = customer.Id;
            return CreatedAtAction("CreateCustomer", new { id = customerdto.Id }, customerdto);
        }

        // PUT /api/customers/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerInDb = await _context.Customers.Where(c => c.Id == id).SingleOrDefaultAsync();

            if (customerInDb == null) return NotFound();
            
            // The context can track changes in customerInDb
            var customer = _mapper.Map(customerdto, customerInDb);

            /*customerInDb.Name = customerdto.Name;
            customerInDb.Birthday = customerdto.Birthday;
            customerInDb.IsSubscribedToNewsletter = customerdto.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customerdto.MembershipTypeId;*/

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerInDb = await _context.Customers.Where(c => c.Id == id).SingleOrDefaultAsync();

            if (customerInDb == null) return NotFound();

            _context.Customers.Remove(customerInDb);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}