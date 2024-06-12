using OsDsII.api.Dtos.ServiceOrders;
using OsDsII.api.Models;

namespace OsDsII.api.Services.Comments
{
    public interface ICommentsService
    {
        public Task<ServiceOrderDto> GetServiceOrderWithComments(int serviceOrderId);
    }
}
