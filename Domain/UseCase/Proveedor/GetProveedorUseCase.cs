
using Domain.Entities;
using Domain.Interfaces.Repositories.Proveedor;
using Domain.Interfaces.UseCases.Proveedor;

namespace Domain.UseCases.Proveedor
{
    public class GetProveedorUseCase : IGetProveedorUseCase
    {
        // Caso de uso: obtener todos los proveedores
        // Inyecta IGetProveedorRepo
        private readonly IGetProveedorRepo _getProveedorRepo;

        public GetProveedorUseCase(IGetProveedorRepo getProveedorRepo)
        {
            _getProveedorRepo = getProveedorRepo;
        }

        public async Task<List<Entities.Proveedor>> GetProveedores()
        {
            return await _getProveedorRepo.GetProveedorRepositorio();
        }
    }
}
