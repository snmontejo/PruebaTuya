using Microsoft.AspNetCore.Mvc;
using PruebaTuya.Application.Services;
using PruebaTuya.Domain.Entities;
using PruebaTuya.Domain.Interfaces;
using PruebaTuya.Infrastructure.Repositories;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerRepository _customerRespository;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderService orderService, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _customerRespository = customerRepository;
            _orderRepository = orderRepository;
        }
        
        [HttpPost("guardar")]
        public  IActionResult CrearPedido([FromBody] Order OrderDatos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer =  _customerRespository.GetById(OrderDatos.CustomerId);
            if (customer == null)
                return NotFound("Cliente no encontrado");

            var order = _orderService.CreateOrder(customer, OrderDatos.Items, OrderDatos.Status);

             _orderRepository.Save(order);

            return Ok(order);
        }
        [HttpPost("cancelar/{id}")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var exito =  _orderService.CancelOrder(id);
            if (!exito)
                return BadRequest("No se puede cancelar la orden.");

            return Ok("Orden cancelada exitosamente.");
        }
    }
}
