using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.UseCase.Producto
{
    public interface IGetProductoByIdProveedorUseCase
    {
        Task<List<Entities.Producto>> getProductoByIdProveedor(int idProveedor);
    }
}
