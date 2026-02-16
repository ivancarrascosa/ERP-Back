using Domain.Interfaces.Repositories.Producto;
using Domain.Interfaces.UseCase.Producto;
using Domain.Interfaces.UseCases.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Producto
{
    public class GetProductoByIdProveedorUseCase : IGetProductoByIdProveedorUseCase
    {
        // Caso de uso: obtener todos los productos
        // Inyecta IGetProductoRepo para consultar la BBDD
        private readonly IGetProductoByIdProveedorRepo _getProductoByIdProveedorRepo;

        public GetProductoByIdProveedorUseCase(IGetProductoByIdProveedorRepo getProductoByIdProveedorRepo)
        {
            _getProductoByIdProveedorRepo = getProductoByIdProveedorRepo;
        }

        public Task<List<Entities.Producto>> getProductoByIdProveedor(int idProveedor)
        {
            return _getProductoByIdProveedorRepo.getProductoByIdProveedor(idProveedor);
        }
    }
}
