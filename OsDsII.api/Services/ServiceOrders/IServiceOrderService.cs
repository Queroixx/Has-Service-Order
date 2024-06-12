using OsDsII.api.Dtos.ServiceOrders;

namespace OsDsII.api.Services.ServiceOrders
{
    public interface IServiceOrderService
    {
        public Task<List<ServiceOrderDto>> GetAllAsync();
        public Task<ServiceOrderDto> GetServiceOrderAsync(int id);
        public Task<NewServiceOrderDto> CreateServiceOrderAsync(CreateServiceOrderDto createServiceOrderDto);
        public Task FinishServiceOrderAsync(int id);
        public Task CancelServiceOrderAsync(int id);
    }
}
