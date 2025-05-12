using PruebaTuya.Domain.Entities;
using PruebaTuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Application.Services
{

    public class OrderService(IOrderRepository repository) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;

        public Order CreateOrder(Customer customer, List<OrderItem> items, string Status)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (items == null || !items.Any())
                throw new ArgumentException("Order must contain at least one item.");

            var order = new Order(customer.Id);

            foreach (var item in items)
            {
                order.AddItem(item);
            }
            order.Status = Status;
            return order;
        }

        public bool  CancelOrder(int Idorder)
        {
            // verificar si el pedido es cancelable
            var orden =  _repository.GetById(Idorder);

            if (orden == null || orden.Status == "Cancelada" || orden.Status == "Enviada")
            {
                return false;
            }
            orden.Status = "Cancelada";
             _repository.Update(orden);
            return true;
        }
    }
}
