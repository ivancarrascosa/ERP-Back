// Repositorio: actualiza unicamente el campo Estado de un pedido
// Recibe el id del pedido y el nuevo valor del estado
// Devuelve true si se afecto al menos una fila (actualizacion exitosa)
using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;
using System.ComponentModel;


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
            if (estado >= 0 && estado <= 3)
            {
                using var connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                // UPDATE solo del campo Estado, filtrado por IdPedido
                string query = @"
                    UPDATE Pedido
                    SET Estado = @Estado 
                    WHERE IdPedido = @IdPedido";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Estado", estado);
                command.Parameters.AddWithValue("@IdPedido", idPedido);

                // Si se afecto 1 o mas filas, la actualizacion fue exitosa
                int filasAfectadas = await command.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
            else
            {
                throw new InvalidEnumArgumentException("El valor del estado debe ser entre 0 y 3.");
            }
        }
    }
}
