
using Domain.DTOs;

namespace Domain.Interfaces.Repositories.Pedido
{

    // Interfaz del repositorio para obtener todos los pedidos
    // Devuelve el DTO PedidoConNombreProveedor (JOIN Pedidos + Proveedores)
    public interface IGetPedidoRepo
    {
        
        Task<List<PedidoConNombreProveedor>> GetPedidoRepositorio();
    }
}
