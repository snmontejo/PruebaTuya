using PruebaTuya.Domain.Entities;
using PruebaTuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PruebaTuya.Domain.Entities;
using PruebaTuya.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTuya.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(Guid id)
        {
            var customer = _repository.GetById(id);
            return customer != null ? Ok(customer) : NotFound();
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            _repository.Save(customer);
            return Ok();
        }
    }
}
