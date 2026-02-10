
namespace Domain.Interfaces.UseCases.Producto
{
    public interface IUpdateStockProductoUseCase
    {
        // Interfaz del caso de uso para actualizar el stock de un producto
        Task<bool> ActualizarStockProductos(int idProducto, int cantidad);
    }
}
