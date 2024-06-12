using AutoMapper;
using Moq;
using OsDsII.api.Dtos;
using OsDsII.api.Dtos.Customers;
using OsDsII.api.Exceptions;
using OsDsII.api.Models;
using OsDsII.api.Repository;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Services.Customers;


namespace CalculadoraSalario.Tests
{
    public class CustomersServiceTests
    {
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CustomersService _service;
        private List<CustomerDto> _lista;

        public CustomersServiceTests()
        {
            _mockCustomersRepository = new Mock<ICustomersRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CustomersService(_mockCustomersRepository.Object, _mockMapper.Object);
            _lista = new List<CustomerDto>();

        }


        [Fact]
        public async Task GetCustomerAsync_CustomerExists_ReturnsCustomerDto()
        {
            
            var customer = new Customer(1, "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168");
            var customerDto = new CustomerDto( "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168",null);
            
            _mockCustomersRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(customer);

            _mockMapper.Setup(mapper => mapper.Map<CustomerDto>(customer))
                .Returns(customerDto);

            
            var result = await _service.GetCustomerAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerDto, result);
        }

        [Fact]
        public async Task GetCustomerAsync_CustomerNotFound_ThrowsNotFoundException()
        {
            
            _mockCustomersRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Customer)null);

             
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetCustomerAsync(1));
        }

        [Fact]
        public async Task CreateAsync_CustomerDoesNotExist_AddsCustomer()
        {
            
            var criasCustomerDto = new CreateCustomerDto("Cristiano Ronaldo", new object(), "cristiano.ronaldo@gmail.com", "192168");
            var customer = new Customer(0, "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168");

            _mockMapper.Setup(m => m.Map<Customer>(criasCustomerDto)).Returns(customer);
            _mockCustomersRepository.Setup(repo => repo.FindUserByEmailAsync(criasCustomerDto.Email))
                .ReturnsAsync((Customer)null);
            _mockCustomersRepository.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);

            
            await _service.CreateAsync(criasCustomerDto);

            // Assert
            _mockCustomersRepository.Verify(repo => repo.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CustomerAlreadyExists_ThrowsConflictException()
        {
            
            var createCustomerDto = new CreateCustomerDto("Cristiano Ronaldo", new object(), "cristiano.ronaldo@gmail.com", "192168");
            var existingCustomer = new Customer(1, "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168");
            var customer = new Customer(0, "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168");

            _mockMapper.Setup(m => m.Map<Customer>(createCustomerDto)).Returns(customer);
            _mockCustomersRepository.Setup(repo => repo.FindUserByEmailAsync(createCustomerDto.Email))
                .ReturnsAsync(existingCustomer);

             
            var exception = await Assert.ThrowsAsync<ConflictException>(() => _service.CreateAsync(createCustomerDto));
            Assert.Equal("Customer already exists", exception.Message);
        }


        [Fact]
        public async Task UpdateAsync_CustomerExists_UpdatesCustomer()
        {
            
            var atualizaCustomerDto = new CreateCustomerDto("Cristiano Ronaldo Atualizado", "Cristiano Ronaldo Atualizado", "john.doe.Atualizado@example.com", "0987654321");
            var customerExistente = new Customer(1, "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168");
            _mockCustomersRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(customerExistente);

            
            await _service.UpdateAsync(1, atualizaCustomerDto);

            _mockCustomersRepository.Verify(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Once);
            Assert.Equal("Cristiano Ronaldo Atualizado", customerExistente.Name);
            Assert.Equal("cristiano.ronaldo2@gmail.com", customerExistente.Email);
            Assert.Equal("192168", customerExistente.Phone);
        }

        [Fact]
        public async Task UpdateAsync_CustomerNotFound_ThrowsNotFoundException()
        {
            
            var atualizaCustomerDto = new CreateCustomerDto("Cristiano Ronaldo Atualizado", "Cristiano Ronaldo Atualizado", "cristiano.ronaldo2@gmail.com", "1921682");
            _mockCustomersRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Customer)null);

             
            await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(1, atualizaCustomerDto));
        }

        [Fact]
        public async Task DeleteAsync_CustomerExists_DeletesCustomer()
        {
            
            var CustomerExistente = new Customer(1, "Cristiano Ronaldo", "cristiano.ronaldo@gmail.com", "192168");
            _mockCustomersRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(CustomerExistente);

            

            await _service.DeleteAsync(1);

            _mockCustomersRepository.Verify(repo => repo.DeleteCustomer(CustomerExistente), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CustomerNotFound_ThrowsNotFoundException()
        {
            
            _mockCustomersRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Customer)null);

             
            await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(1));
        }
    }
}