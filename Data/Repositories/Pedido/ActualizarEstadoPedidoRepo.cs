// Repositorio: actualiza unicamente el campo Estado de un pedido
// Recibe el id del pedido y el nuevo valor del estado
// Devuelve true si se afecto al menos una fila (actualizacion exitosa)
using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using MySql.Data.MySqlClient;


namespace Data.Repositories.Pedido
{
    public class ActualizarEstadoPedidoRepo : IActualizarEstadoPedidoRepo
    {
        private readonly Conexion _conexion;

        public ActualizarEstadoPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<bool> ActualizarEstadoPedidoRepositorio(int idPedido, int estado)
        {
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // UPDATE solo del campo Estado, filtrado por IdPedido
            string query = @"
                UPDATE Pedidos 
                SET Estado = @Estado 
                WHERE IdPedido = @IdPedido";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Estado", estado);
            command.Parameters.AddWithValue("@IdPedido", idPedido);

            // Si se afecto 1 o mas filas, la actualizacion fue exitosa
            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
