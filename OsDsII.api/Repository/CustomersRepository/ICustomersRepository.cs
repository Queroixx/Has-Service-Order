using OsDsII.api.Models;

namespace OsDsII.api.Repository.CustomersRepository
{
    public interface ICustomersRepository
    {
        public Task<IEnumerable<Customer>> GetAllAsync();
        public Task<Customer> GetByIdAsync(int id);
        public Task AddCustomerAsync(Customer customer);
        public Task DeleteCustomer(Customer customer);
        public Task UpdateCustomerAsync(Customer customer);
        public Task<Customer> FindUserByEmailAsync(string email);
    }
}
