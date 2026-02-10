
using Domain.DTOs;
using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.UseCases.Pedido;

namespace Domain.UseCases.Pedido
{
    // Caso de uso: obtener todos los pedidos
    // Inyecta el repositorio IGetPedidoRepo via constructor (Dependency Injection)
    // Simplemente delega la llamada al repositorio
    public class GetPedidoUseCase : IGetPedidoUseCase
    {
        // Repositorio inyectado por constructor
        private readonly IGetPedidoRepo _getPedidoRepo;

        public GetPedidoUseCase(IGetPedidoRepo getPedidoRepo)
        {
            _getPedidoRepo = getPedidoRepo;
        }

        // Ejecuta la logica de negocio (en este caso, delega al repo)
        public async Task<List<PedidoConNombreProveedor>> GetPedido()
        {
            return await _getPedidoRepo.GetPedidoRepositorio();
        }
    }
}
