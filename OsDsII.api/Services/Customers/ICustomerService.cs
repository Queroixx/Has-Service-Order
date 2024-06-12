using OsDsII.api.Dtos.Customers;

namespace OsDsII.api.Services.Customers
{
    public interface ICustomersService
    {
        public Task<IEnumerable<CustomerDto>> GetAllAsync();
        public Task<CustomerDto> GetCustomerAsync(int id);
        public Task CreateAsync(CreateCustomerDto customer);
        public Task UpdateAsync(int id, CreateCustomerDto customerDto);
        public Task DeleteAsync(int id);
    }
}