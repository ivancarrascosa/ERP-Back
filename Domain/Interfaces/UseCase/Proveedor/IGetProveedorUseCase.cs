
using Domain.Entities;

namespace Domain.Interfaces.UseCases.Proveedor
{
    public interface IGetProveedorUseCase
    {
        // Interfaz del caso de uso para obtener todos los proveedores
        Task<List<Entities.Proveedor>> GetProveedores();
    }
}
