using PruebaTuya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
        IEnumerable<Customer> GetAll();
        void Save(Customer customer);
        void Update(Customer customer);
        void Delete(int id);

        
    }
}
