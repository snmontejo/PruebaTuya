using PruebaTuya.Domain.Entities;
using PruebaTuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PruebaTuya.Infrastructure.Repositories;

namespace Presentation.Controllers
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
        [HttpGet("All")]
        public ActionResult<Customer> GetAllCostumers()
        {
            var customer = _repository.GetAll();
            return customer != null ? Ok(customer) : NotFound();
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<Customer> GetByIdCostumer(int id)
        {
            var customer = _repository.GetById(id);
            return customer != null ? Ok(customer) : NotFound();
        }
        [HttpPut("Update/{id}")]
        public  IActionResult UpdateCustomer(int id, [FromBody] Customer customerDatos)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer =  _repository.GetById(id);
            if (customer == null)
            {
                return NotFound("Cliente no encontrado");
            }
            else
            {
                // Actualizar los campos
             customer.Name = customerDatos.Name;
             customer.Email = customerDatos.Email;
                _repository.Update(customer);
            }
            return Ok("Cliente actualizado correctamente");
        }

        [HttpPost("Save")]
        public IActionResult SaveCostumer(Customer customer)
        {
            _repository.Save(customer);
            return Ok();
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteCostumer(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
