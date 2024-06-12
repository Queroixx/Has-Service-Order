using Microsoft.EntityFrameworkCore;
using OsDsII.api.Data;
using OsDsII.api.Models;

namespace OsDsII.api.Repository.ServiceOrderRepository
{
    public sealed class ServiceOrderRepository : IServiceOrderRepository
    {
        // DI DATA CONTEXT
        private readonly DataContext _dataContext;

        public ServiceOrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<ServiceOrder>> GetAllAsync()
        {
            return await _dataContext.ServiceOrders.ToListAsync();
        }

        public async Task<ServiceOrder> GetByIdAsync(int id)
        {
            return await _dataContext.ServiceOrders.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(ServiceOrder serviceOrder)
        {
            await _dataContext.ServiceOrders.AddAsync(serviceOrder);
            await _dataContext.SaveChangesAsync();
        }

        public async Task FinishAsync(ServiceOrder serviceOrder)
        {
            _dataContext.ServiceOrders.Update(serviceOrder);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CancelAsync(ServiceOrder serviceOrder)
        {
            _dataContext.ServiceOrders.Update(serviceOrder);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ServiceOrder> GetServiceOrderWithComments(int serviceOrderId)
        {
            return await _dataContext.ServiceOrders
                .Include(c => c.Customer)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(s => s.Id == serviceOrderId);
        }

        public async Task<ServiceOrder> GetServiceOrderFromUser(int serviceOrderId)
        {
            return await _dataContext.ServiceOrders
                 .Include(c => c.Customer)
                 .FirstOrDefaultAsync(s => serviceOrderId == s.Id);
        }

    }
}
