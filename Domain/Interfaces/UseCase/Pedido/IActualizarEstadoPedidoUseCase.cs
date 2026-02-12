
namespace Domain.Interfaces.UseCases.Pedido
{
    // Interfaz del caso de uso para actualizar el estado de un pedido
    public interface IActualizarEstadoPedidoUseCase
    {
        Task<bool> ActualizarEstadoPedido(int idPedido, int estado);
    }
}
