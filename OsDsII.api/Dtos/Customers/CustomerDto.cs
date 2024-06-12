using OsDsII.api.Dtos.ServiceOrders;

namespace OsDsII.api.Dtos.Customers
{
    public record CustomerDto(
        string Name,
        string Email,
        string Phone,
        List<ServiceOrderDto> ListServiceOrder);
}
