
using Domain.DTOs;

namespace Domain.Interfaces.Repositories.PedidoCompleto
{
    public interface IGetPedidoCompletoRepo
    {
        // Interfaz del repositorio para obtener un pedido completo (factura)
        // Hace JOIN de Pedidos + Proveedores + Usuarios + DetallesPedido + Productos
        // Devuelve el DTO PedidoConDetalles
        Task<PedidoConDetalles?> GetPedidoCompletoRepo(int idPedido);
    }
}
