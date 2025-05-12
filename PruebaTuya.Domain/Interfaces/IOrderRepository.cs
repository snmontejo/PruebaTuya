using PruebaTuya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Order? GetById(int orderId);
        //IEnumerable<Order> GetByCustomerId(Guid customerId);
        void Save(Order order);
        void Update(Order order);
        //void Delete(Guid orderId);
        
    }
}
