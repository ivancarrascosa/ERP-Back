// Caso de uso: crear un nuevo pedido
// Inyecta IPostPedidoRepo para insertar el pedido en la BBDD
using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.UseCases.Pedido;

namespace Domain.UseCases.Pedido
{
    public class PostPedidoUseCase : IPostPedidoUseCase
    {
        private readonly IPostPedidoRepo _postPedidoRepo;

        public PostPedidoUseCase(IPostPedidoRepo postPedidoRepo)
        {
            _postPedidoRepo = postPedidoRepo;
        }

        // Recibe la entidad Pedido y devuelve el ID generado por la BBDD
        public async Task<int> CrearPedido(Entities.Pedido pedido)
        {
            return await _postPedidoRepo.CrearPedidoRepositorio(pedido);
        }
    }
}
