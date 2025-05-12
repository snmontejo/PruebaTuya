using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTuya.Domain.Entities
{
    public class Customer
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public DateTime RegisteredAt { get; private set; }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
            RegisteredAt = DateTime.UtcNow;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            Name = name;
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email.");

            Email = email;
        }
    }
}