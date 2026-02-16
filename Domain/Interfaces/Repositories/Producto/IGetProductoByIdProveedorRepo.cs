using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Producto
{
    public interface IGetProductoByIdProveedorRepo
    {
        Task<List<Entities.Producto>> getProductoByIdProveedor(int idProveedor);
    }
}
