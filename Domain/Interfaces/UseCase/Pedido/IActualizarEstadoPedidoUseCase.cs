// Interfaz del caso de uso para actualizar el estado de un pedido
namespace Domain.Interfaces.UseCases.Pedido
{
    public interface IActualizarEstadoPedidoUseCase
    {
        Task<bool> ActualizarEstadoPedido(int idPedido, int estado);
    }
}
