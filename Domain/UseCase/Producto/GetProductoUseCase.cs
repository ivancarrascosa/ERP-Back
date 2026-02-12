
using Domain.Entities;
using Domain.Interfaces.Repositories.Producto;
using Domain.Interfaces.UseCases.Producto;

namespace Domain.UseCases.Producto
{
    public class GetProductoUseCase : IGetProductoUseCase
    {
        // Caso de uso: obtener todos los productos
        // Inyecta IGetProductoRepo para consultar la BBDD
        private readonly IGetProductoRepo _getProductoRepo;

        public GetProductoUseCase(IGetProductoRepo getProductoRepo)
        {
            _getProductoRepo = getProductoRepo;
        }

        public async Task<List<Entities.Producto>> GetProductos()
        {
            return await _getProductoRepo.GetProductoRepositorio();
        }
    }
}
