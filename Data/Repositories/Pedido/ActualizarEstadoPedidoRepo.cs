using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace Data.Repositories.Pedido
{
    public class ActualizarEstadoPedidoRepo : IActualizarEstadoPedidoRepo
    {
        private readonly Conexion _conexion;

        public ActualizarEstadoPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Actualiza el estado de un pedido específico en la base de datos.
        /// </summary>
        /// <param name="idPedido">El identificador único del pedido a actualizar.</param>
        /// <param name="estado">El nuevo valor numérico del estado (debe estar entre 0 y 3).</param>
        /// <returns>Devuelve true si se actualizó al menos una fila; false en caso contrario.</returns>
        /// <exception cref="ArgumentException">Se lanza si el idPedido es menor o igual a 0.</exception>
        /// <exception cref="InvalidEnumArgumentException">Se lanza si el estado no está dentro del rango permitido (0-3).</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error general o de base de datos durante la ejecución.</exception>
        public async Task<bool> ActualizarEstadoPedidoRepositorio(int idPedido, int estado)
        {
         
            // Validamos los datos de entrada antes de abrir conexiones o procesar nada.
            if (idPedido <= 0)
            {
                throw new ArgumentException("El identificador del pedido no es válido.", nameof(idPedido));
            }

            if (estado < 0 || estado > 3)
            {
                throw new InvalidEnumArgumentException(nameof(estado), estado, typeof(int));
            }

            // Consulta SQL constante para evitar recrear el string múltiples veces
            const string query = @"
                UPDATE Pedido
                SET Estado = @Estado 
                WHERE IdPedido = @IdPedido";

            try
            {
                // Reemplazo de var por tipos explícitos
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(query, connection);

               
                command.Parameters.Add("@Estado", SqlDbType.Int).Value = estado;
                command.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;

                int filasAfectadas = await command.ExecuteNonQueryAsync();

                return filasAfectadas > 0;
            }
            catch (SqlException ex)
            {
                
                // throw new RepositoryException("Error en base de datos al actualizar el pedido", ex);
                throw new Exception($"Error de base de datos al actualizar el pedido {idPedido}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al actualizar el pedido {idPedido}.", ex);
            }
        }
    }
}