using AutoMapper;
using OsDsII.api.Dtos.ServiceOrders;
using OsDsII.api.Models;
using OsDsII.api.Repository.CommentsRepository;
using OsDsII.api.Repository.ServiceOrderRepository;

namespace OsDsII.api.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepository commentsRepository, IMapper mapper, IServiceOrderRepository serviceOrderRepository)
        {
            _commentsRepository = commentsRepository;
            _serviceOrderRepository = serviceOrderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceOrderDto> GetServiceOrderWithComments(int serviceOrderId)
        {
            ServiceOrder serviceOrderWithComments = await _serviceOrderRepository.GetServiceOrderWithComments(serviceOrderId);
            var serviceOrder = _mapper.Map<ServiceOrderDto>(serviceOrderWithComments);
            return serviceOrder;
        }
    }
}
