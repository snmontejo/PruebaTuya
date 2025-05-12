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
        void Save(Order order);
        void Update(Order order);
        
    }
}
