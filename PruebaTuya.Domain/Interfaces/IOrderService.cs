using PruebaTuya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Domain.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(Customer customer, List<OrderItem> items, string Status);
        bool CancelOrder(int Idorder);
        
    }
}
