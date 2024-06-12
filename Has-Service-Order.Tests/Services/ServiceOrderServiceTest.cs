using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Moq;
using OsDsII.api.Dtos;
using OsDsII.api.Dtos.ServiceOrders;
using OsDsII.api.Exceptions;
using OsDsII.api.Models;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Repository.ServiceOrderRepository;
using OsDsII.api.Services.ServiceOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Has_Service_Order.Tests.Services
{
    public class ordensServicoerviceTest
    {
        private readonly Mock<IServiceOrderRepository> _serviceOrderRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICustomersRepository> _serviceCustomerMock;
        private readonly ServiceOrderService _service;

        public ordensServicoerviceTest()
        {
            _serviceOrderRepositoryMock = new Mock<IServiceOrderRepository>();
            _mapperMock = new Mock<IMapper>();
            _serviceCustomerMock = new Mock<ICustomersRepository>();
            _service = new ServiceOrderService(_serviceOrderRepositoryMock.Object, _serviceCustomerMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnordensServico()
        {
            
            var ordensServico = new List<ServiceOrder>
            {
                new ServiceOrder { Id = 1, Description = "Serviço 1", Price = 100, Status = StatusServiceOrder.OPEN },
                new ServiceOrder { Id = 2, Description = "Serviço 2", Price = 200, Status = StatusServiceOrder. OPEN }
            };

            var serviceOrderDtos = new List<ServiceOrderDto>
            {
                new ServiceOrderDto(1, "Serviço 1", 100, StatusServiceOrder.OPEN, DateTimeOffset.Now, DateTimeOffset.Now.AddHours(1), new List<CommentDto>()),
                new ServiceOrderDto(2, "Serviço 2", 200, StatusServiceOrder.OPEN, DateTimeOffset.Now, DateTimeOffset.Now.AddHours(2), new List<CommentDto>())
            };

            _serviceOrderRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ordensServico);
            _mapperMock.Setup(mapper => mapper.Map<List<ServiceOrderDto>>(ordensServico)).Returns(serviceOrderDtos);

            
            var result = await _service.GetAllAsync();

            
            Assert.Equal(serviceOrderDtos, result);
        }

        [Fact]
        public async Task GetServiceOrderAsync_ShouldReturnServiceOrderDto_WhenServiceOrderExists()
        {
            
            var serviceOrder = new ServiceOrder { Id = 1, Description = "Serviço 1", Price = 100, Status = StatusServiceOrder.OPEN };
            var serviceOrderDto = new ServiceOrderDto(1, "Serviço 1", 100, StatusServiceOrder.OPEN, DateTimeOffset.Now, DateTimeOffset.Now.AddHours(1), new List<CommentDto>());

            _serviceOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(serviceOrder);
            _mapperMock.Setup(mapper => mapper.Map<ServiceOrderDto>(serviceOrder)).Returns(serviceOrderDto);

            
            var result = await _service.GetServiceOrderAsync(1);

            
            Assert.Equal(serviceOrderDto, result);
        }

        [Fact]
        public async Task GetServiceOrderAsync_ShouldThrowNotFoundException_WhenServiceOrderDoesNotExist()
        {
            
            _serviceOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ServiceOrder)null);

             
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetServiceOrderAsync(1));
        }

        [Fact]
        public async Task CreateServiceOrderAsync_ShouldCreateAndReturnNewServiceOrderDto()
        {
            
            var createServiceOrderDto = new CreateServiceOrderDto("Novo Serviço", 300, 1);
            var customer = new Customer { Id = 1 };
            var serviceOrder = new ServiceOrder { Id = 1, Description = "Novo Serviço", Price = 300, Status = StatusServiceOrder.OPEN, OpeningDate = DateTimeOffset.Now };
            var newServiceOrderDto = new NewServiceOrderDto(1, "Novo Serviço", 300, StatusServiceOrder.OPEN, DateTimeOffset.Now, null, 1);

            _serviceCustomerMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
            _mapperMock.Setup(mapper => mapper.Map<ServiceOrder>(createServiceOrderDto)).Returns(serviceOrder);
            _serviceOrderRepositoryMock.Setup(repo => repo.AddAsync(serviceOrder)).Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<NewServiceOrderDto>(serviceOrder)).Returns(newServiceOrderDto);

            
            var result = await _service.CreateServiceOrderAsync(createServiceOrderDto);

            
            Assert.Equal(newServiceOrderDto, result);
        }

        [Fact]
        public async Task CreateServiceOrderAsync_ShouldThrowBadRequest_WhenCustomerDoesNotExist()
        {
            
            var createServiceOrderDto = new CreateServiceOrderDto("Novo Serviço", 300, 1);

            _serviceCustomerMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Customer)null);

             
            await Assert.ThrowsAsync<BadRequest>(() => _service.CreateServiceOrderAsync(createServiceOrderDto));
        }

        [Fact]
        public async Task FinishServiceOrderAsync_ShouldFinishServiceOrder_WhenServiceOrderExists()
        {
            
            var serviceOrder = new ServiceOrder { Id = 1, Description = "Serviço 1", Price = 100, Status = StatusServiceOrder.OPEN };

            _serviceOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(serviceOrder);
            _serviceOrderRepositoryMock.Setup(repo => repo.FinishAsync(serviceOrder)).Returns(Task.CompletedTask);

            
            await _service.FinishServiceOrderAsync(1);

            
            _serviceOrderRepositoryMock.Verify(repo => repo.FinishAsync(serviceOrder), Times.Once);
        }

        [Fact]
        public async Task FinishServiceOrderAsync_ShouldThrowNotFoundException_WhenServiceOrderDoesNotExist()
        {
            
            _serviceOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ServiceOrder)null);

             
            await Assert.ThrowsAsync<NotFoundException>(() => _service.FinishServiceOrderAsync(1));
        }

        [Fact]
        public async Task CancelServiceOrderAsync_ShouldCancelServiceOrder_WhenServiceOrderExists()
        {
            
            var serviceOrder = new ServiceOrder { Id = 1, Description = "Serviço 1", Price = 100, Status = StatusServiceOrder.OPEN };

            _serviceOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(serviceOrder);
            _serviceOrderRepositoryMock.Setup(repo => repo.CancelAsync(serviceOrder)).Returns(Task.CompletedTask);

            
            await _service.CancelServiceOrderAsync(1);

            
            _serviceOrderRepositoryMock.Verify(repo => repo.CancelAsync(serviceOrder), Times.Once);
        }

        [Fact]
        public async Task CancelServiceOrderAsync_ShouldThrowNotFoundException_WhenServiceOrderDoesNotExist()
        {
            
            _serviceOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ServiceOrder)null);

             
            await Assert.ThrowsAsync<NotFoundException>(() => _service.CancelServiceOrderAsync(1));
        }


    }
}
