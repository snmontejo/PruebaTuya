using PruebaTuya.Domain.Entities;
using PruebaTuya.Domain.Interfaces;
using PruebaTuya.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public Order GetById(int id)
        {
            return _context.Orders
                           .Find(id); // Carga relacionada si aplica;
        }
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public  void  Save(Order order)
        {
            _context.Orders.Add(order);
             _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
