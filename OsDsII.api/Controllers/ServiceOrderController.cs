using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OsDsII.api.Dtos.ServiceOrders;
using OsDsII.api.Exceptions;
using OsDsII.api.Services.ServiceOrders;

namespace OsDsII.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ServiceOrdersController : ControllerBase
    {
        private readonly IServiceOrderService _serviceOrderService;
        private readonly IMapper _mapper;

        public ServiceOrdersController(IServiceOrderService serviceOrderService, IMapper mapper)
        {
            _serviceOrderService = serviceOrderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllServiceOrderAsync()
        {
            try
            {
                List<ServiceOrderDto> serviceOrders = await _serviceOrderService.GetAllAsync();
                return Ok(serviceOrders);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServiceOrderById(int id)
        {
            try
            {
                ServiceOrderDto serviceOrder = await _serviceOrderService.GetServiceOrderAsync(id);
                return Ok(serviceOrder);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NewServiceOrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateServiceOrderAsync(CreateServiceOrderDto serviceOrderDto)
        {
            try
            {
                await _serviceOrderService.CreateServiceOrderAsync(serviceOrderDto);
                return Created("CreateServiceOrderAsync", serviceOrderDto);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPut("{id}/status/finish")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FinishServiceOrderAsync(int id)
        {
            try
            {
                await _serviceOrderService.FinishServiceOrderAsync(id);
                return NoContent();
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPut("{id}/status/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelServiceOrder(int id)
        {
            try
            {
                await _serviceOrderService.CancelServiceOrderAsync(id);
                return NoContent();
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }
    }
}