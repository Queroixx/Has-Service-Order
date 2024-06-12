namespace OsDsII.api.Dtos.ServiceOrders
{
    public record CreateServiceOrderDto(
        string Description,
        double Price,
        int CustomerId
    );
}
