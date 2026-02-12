
using Domain.Interfaces.Repositories.Producto;
using Domain.Interfaces.UseCases.Producto;

namespace Domain.UseCases.Producto
{
    public class UpdateStockProductoUseCase : IUpdateStockProductoUseCase
    {
        // Caso de uso: actualizar el stock de un producto
        // Inyecta IUpdateStockProductoRepo
        private readonly IUpdateStockProductoRepo _updateStockRepo;

        public UpdateStockProductoUseCase(IUpdateStockProductoRepo updateStockRepo)
        {
            _updateStockRepo = updateStockRepo;
        }

        // Recibe el id del producto y la nueva cantidad de stock
        public async Task<bool> ActualizarStockProductos(int idProducto, int cantidad)
        {
            return await _updateStockRepo.ActualizarStockProductoRepo(idProducto, cantidad);
        }
    }
}
