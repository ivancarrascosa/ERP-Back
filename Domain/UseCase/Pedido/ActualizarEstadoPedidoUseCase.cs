
using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.UseCases.Pedido;

namespace Domain.UseCases.Pedido
{

    // Caso de uso: actualizar el estado de un pedido existente
    // Inyecta IActualizarEstadoPedidoRepo
    public class ActualizarEstadoPedidoUseCase : IActualizarEstadoPedidoUseCase
    {
        private readonly IActualizarEstadoPedidoRepo _actualizarEstadoRepo;

        public ActualizarEstadoPedidoUseCase(IActualizarEstadoPedidoRepo actualizarEstadoRepo)
        {
            _actualizarEstadoRepo = actualizarEstadoRepo;
        }

        // Recibe el id del pedido y el nuevo estado (int)
        // Devuelve true si se actualizo correctamente
        public async Task<bool> ActualizarEstadoPedido(int idPedido, int estado)
        {
            return await _actualizarEstadoRepo.ActualizarEstadoPedidoRepositorio(idPedido, estado);
        }
    }
}
