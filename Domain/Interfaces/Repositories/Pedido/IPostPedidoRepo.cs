
namespace Domain.Interfaces.Repositories.Pedido
{
    public interface IPostPedidoRepo
    {
        // Interfaz del repositorio para insertar un pedido nuevo en la BBDD
        // Recibe la entidad Pedido y devuelve el ID generado
        Task<int> CrearPedidoRepositorio(Entities.Pedido pedido);
    }
}
