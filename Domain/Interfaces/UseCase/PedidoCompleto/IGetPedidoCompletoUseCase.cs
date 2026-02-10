
using Domain.DTOs;

namespace Domain.Interfaces.UseCases.PedidoCompleto
{
    // Interfaz del caso de uso para obtener la factura completa de un pedido
    public interface IGetPedidoCompletoUseCase
    {
        Task<PedidoConDetalles?> CrearPedido(int idPedido);
    }
}
