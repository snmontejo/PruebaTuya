using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public int CustomerId { get;  set; }
        public DateTime OrderDate { get;  set; }
        public List<OrderItem> Items { get; set; } = new();
        public string Status { get; set; } = "Pendiente";
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice ) ;

        public Order(int customerId)
        {
            
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
        }

        public void AddItem(OrderItem orderItem)
        {
            if (orderItem.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (orderItem.TotalPrice <= 0)
                throw new ArgumentException("TotalPrice must be greater than zero.");

            var item = new OrderItem(orderItem.ProductName, orderItem.Quantity, orderItem.UnitPrice);
            Items.Add(item);
        }

        public void RemoveItem(int Id)
        {
            var item = Items.FirstOrDefault(i => i.Id == Id);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
    }
}
