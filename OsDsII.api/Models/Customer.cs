using System.ComponentModel.DataAnnotations;

namespace OsDsII.api.Models
{
    public class Customer
    {
        public int Id;

        public string Name { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Phone { get; set; }

        public List<ServiceOrder>? ServiceOrders { get; set; } = null;

        public Customer()
        { }

        public Customer(string name)
        {
            Name = name;
        }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public Customer(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public override bool Equals(object? obj)
        {
            return obj is Customer customer &&
                   Id == customer.Id &&
                   Name == customer.Name &&
                   Email == customer.Email &&
                   Phone == customer.Phone &&
                   EqualityComparer<List<ServiceOrder>>.Default.Equals(ServiceOrders, customer.ServiceOrders);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, Phone, ServiceOrders);
        }
    }
}