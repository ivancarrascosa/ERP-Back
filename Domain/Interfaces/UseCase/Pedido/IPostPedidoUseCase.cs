
namespace Domain.Interfaces.UseCases.Pedido
{
    // Interfaz del caso de uso para crear un nuevo pedido
    public interface IPostPedidoUseCase
    {
        Task<int> CrearPedido(Entities.Pedido pedido);
    }
}
