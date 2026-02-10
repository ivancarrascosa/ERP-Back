
namespace Domain.Interfaces.Repositories.Pedido
{

    // Interfaz del repositorio para actualizar el campo Estado de un pedido
    // Recibe el id del pedido y el nuevo estado (int)
    public interface IActualizarEstadoPedidoRepo
    {
        Task<bool> ActualizarEstadoPedidoRepositorio(int idPedido, int estado);
    }
}
