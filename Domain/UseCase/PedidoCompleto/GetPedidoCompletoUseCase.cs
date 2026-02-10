
using Domain.DTOs;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.UseCases.PedidoCompleto;

namespace Domain.UseCases.PedidoCompleto
{
    public class GetPedidoCompletoUseCase : IGetPedidoCompletoUseCase
    {
        // Caso de uso: obtener la factura completa de un pedido
        // Inyecta IGetPedidoCompletoRepo que hace el JOIN completo en BBDD
        private readonly IGetPedidoCompletoRepo _getPedidoCompletoRepo;

        public GetPedidoCompletoUseCase(IGetPedidoCompletoRepo getPedidoCompletoRepo)
        {
            _getPedidoCompletoRepo = getPedidoCompletoRepo;
        }

        // Recibe el id del pedido y devuelve el DTO PedidoConDetalles
        // (puede ser null si el pedido no existe)
        public async Task<PedidoConDetalles?> GetPedidoCompleto(int idPedido)
        {
            return await _getPedidoCompletoRepo.GetPedidoCompletoRepositorio(idPedido);
        }
    }
}
