using Domain.Entities;

namespace Domain.Interfaces.UseCases.Producto
{
    public interface IGetProductoUseCase
    {
        // Interfaz del caso de uso para obtener todos los productos

        Task<List<Entities.Producto>> GetProductos();
    }
}
