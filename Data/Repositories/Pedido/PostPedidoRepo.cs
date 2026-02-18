using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.Pedido
{
    public class PostPedidoRepo : IPostPedidoRepo
    {
        private readonly Conexion _conexion;

        public PostPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla Pedido y devuelve el ID generado.
        /// </summary>
        /// <param name="pedido">La entidad Pedido con los datos a insertar.</param>
        /// <returns>El ID (entero) del nuevo pedido creado.</returns>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto pedido es nulo.</exception>
        /// <exception cref="ArgumentException">Se lanza si los datos del pedido no son válidos (total negativo, proveedor inválido, etc).</exception>
        /// <exception cref="Exception">Se lanza ante errores de conexión o ejecución SQL.</exception>
        public async Task<int> CrearPedidoRepositorio(Domain.Entities.Pedido pedido)
        {
          
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido), "El objeto pedido no puede ser nulo.");
            }

            if (pedido.TotalPedido < 0)
            {
                throw new ArgumentException("El total del pedido no puede ser negativo.", nameof(pedido.TotalPedido));
            }

            if (string.IsNullOrEmpty(pedido.FirebaseUID))
            {
                throw new ArgumentException("El ID de usuario (FirebaseUID) es obligatorio.", nameof(pedido.FirebaseUID));
            }

            string query = @"
                INSERT INTO Pedido (FechaPedido, IdProveedor, TotalPedido, Estado, IdUsuario)
                VALUES (@FechaPedido, @IdProveedor, @TotalPedido, @Estado, @IdUsuario);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            try
            {
             
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(query, connection);

              
                command.Parameters.Add("@FechaPedido", SqlDbType.DateTime).Value = pedido.FechaPedido;
                command.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = pedido.IdProveedor;
                command.Parameters.Add("@TotalPedido", SqlDbType.Decimal).Value = pedido.TotalPedido;
                command.Parameters.Add("@Estado", SqlDbType.Int).Value = pedido.Estado;

             
                command.Parameters.Add("@IdUsuario", SqlDbType.NVarChar).Value = pedido.FirebaseUID;

              
                object result = await command.ExecuteScalarAsync();

                // Validación de que el resultado no sea nulo antes de convertir
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                return 0;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al crear el pedido: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al crear el pedido.", ex);
            }
        }
    }
}