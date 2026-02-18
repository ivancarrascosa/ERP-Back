using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.Pedido
{
    public class EliminarPedidoRepo : IEliminarPedidoRepo
    {
        private readonly Conexion _conexion;

        public EliminarPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Realiza un borrado lógico de un pedido estableciendo su campo 'Borrado' a 1.
        /// </summary>
        /// <param name="idPedido">El identificador único del pedido a eliminar.</param>
        /// <returns>Devuelve true si el pedido fue encontrado y marcado como borrado; false si no existía.</returns>
        /// <exception cref="ArgumentException">Se lanza si el idPedido es menor o igual a 0.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error de conexión o ejecución en la base de datos.</exception>
        public async Task<bool> eliminarPedido(int idPedido)
        {
          
            // Validamos que el ID tenga sentido antes de intentar conectar a la BD.
            if (idPedido <= 0)
            {
                throw new ArgumentException("El identificador del pedido no es válido.", nameof(idPedido));
            }

            // Consulta SQL de borrado lógico
            const string query = @"
                UPDATE Pedido
                SET Borrado = 1 
                WHERE IdPedido = @IdPedido";

            try
            {
               
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(query, connection);

               
                command.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;

                int filasAfectadas = await command.ExecuteNonQueryAsync();

                return filasAfectadas > 0;
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores de tipo SQL
                throw new Exception($"Error de base de datos al intentar eliminar el pedido {idPedido}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de errores mas generales
                throw new Exception($"Ocurrió un error inesperado al eliminar el pedido {idPedido}.", ex);
            }
        }
    }
}