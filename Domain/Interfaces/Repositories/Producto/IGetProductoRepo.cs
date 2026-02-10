
using Domain.Entities;

namespace Domain.Interfaces.Repositories.Producto
{
    public interface IGetProductoRepo
    {
        // Interfaz del repositorio para obtener todos los productos
        // Devuelve la lista completa de productos desde la BBDD
        Task<List<Entities.Producto>> GetProductoRepo();
    }
}
