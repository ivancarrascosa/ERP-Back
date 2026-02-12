
using Domain.Entities;

namespace Domain.Interfaces.Repositories.Proveedor
{
    public interface IGetProveedorRepo
    {
        // Interfaz del repositorio para obtener todos los proveedores
        // Devuelve la lista completa de proveedores desde la BBDD
        Task<List<Entities.Proveedor>> GetProveedorRepositorio();
    }
}
