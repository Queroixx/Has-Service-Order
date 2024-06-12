using AutoMapper;
using Moq;
using OsDsII.api.Dtos;
using OsDsII.api.Dtos.ServiceOrders;
using OsDsII.api.Models;
using OsDsII.api.Repository.CommentsRepository;
using OsDsII.api.Repository.ServiceOrderRepository;
using OsDsII.api.Services.Comments;
using OsDsII.api.Services.ServiceOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Has_Service_Order.Tests.Services
{
    public class CommentsServiceTest
    {
        private readonly Mock<ICommentsRepository> _mockCommentsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CommentsService _service;
        private List<CommentDto> _lista;
        private readonly Mock<IServiceOrderRepository> _mockServiceOrderRepository;

        public CommentsServiceTest()
        {
            _mockCommentsRepository = new Mock<ICommentsRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockServiceOrderRepository = new Mock<IServiceOrderRepository>();
            _service = new CommentsService(_mockCommentsRepository.Object, _mockMapper.Object, _mockServiceOrderRepository.Object);
            _lista = new List<CommentDto>();
        }

        [Fact]
        public async Task GetServiceOrderWithComments_ShouldReturnServiceOrderDto()
        {
            // Arrange
            int serviceOrderId = 1;

            var comments = new List<Comment>
        {
            new Comment { Id = 1, Description = "Comentário Simples", SendDate = DateTimeOffset.Now, ServiceOrderId = serviceOrderId }
        };

            var serviceOrder = new ServiceOrder
            {
                Id = serviceOrderId,
                Description = "Descrição Simples",
                Price = 100.0,
                Status = StatusServiceOrder.OPEN,
                OpeningDate = DateTimeOffset.Now,
                FinishDate = DateTimeOffset.Now.AddDays(1),
                Comments = comments
            };

         var commentsDto = new List<CommentDto>
        {
            new CommentDto(1, "Comentário Simples", DateTimeOffset.Now, serviceOrderId)
        };

            var serviceOrderDto = new ServiceOrderDto(
                serviceOrderId,
                "Descrição Simples",
                100.0,
                StatusServiceOrder.OPEN,
                serviceOrder.OpeningDate,
                serviceOrder.FinishDate,
                commentsDto
            );

            _mockServiceOrderRepository
                .Setup(repo => repo.GetServiceOrderWithComments(serviceOrderId))
                .ReturnsAsync(serviceOrder);

            _mockMapper
                .Setup(mapper => mapper.Map<ServiceOrderDto>(serviceOrder))
                .Returns(serviceOrderDto);

            // Act
            var result = await _service.GetServiceOrderWithComments(serviceOrderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(serviceOrderDto.Id, result.Id);
            Assert.Equal(serviceOrderDto.Description, result.Description);
            Assert.Equal(serviceOrderDto.Price, result.Price);
            Assert.Equal(serviceOrderDto.Status, result.Status);
            Assert.Equal(serviceOrderDto.OpeningDate, result.OpeningDate);
            Assert.Equal(serviceOrderDto.FinishDate, result.FinishDate);
            Assert.Equal(serviceOrderDto.Comments.Count, result.Comments.Count);

            var expectedComment = serviceOrderDto.Comments[0];
            var actualComment = result.Comments[0];

            Assert.Equal(expectedComment.Id, actualComment.Id);
            Assert.Equal(expectedComment.Description, actualComment.Description);
            Assert.Equal(expectedComment.SendDate, actualComment.SendDate);
            Assert.Equal(expectedComment.ServiceOrderId, actualComment.ServiceOrderId);
        }
    }
}

