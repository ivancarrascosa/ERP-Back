
using Domain.DTOs;

namespace Domain.Interfaces.UseCases.Pedido
{// Interfaz del caso de uso para obtener todos los pedidos
    public interface IGetPedidoUseCase
    {
        Task<List<PedidoConNombreProveedor>> GetPedido();
    }
}
