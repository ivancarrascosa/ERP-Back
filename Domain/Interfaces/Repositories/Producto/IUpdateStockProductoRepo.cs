
namespace Domain.Interfaces.Repositories.Producto
{
    public interface IUpdateStockProductoRepo
    {
        // Interfaz del repositorio para actualizar el stock de un producto
        // Recibe el id del producto y la nueva cantidad de stock
        Task<bool> ActualizarStockProductoRepo(int idProducto, int cantidad);
    }
}
