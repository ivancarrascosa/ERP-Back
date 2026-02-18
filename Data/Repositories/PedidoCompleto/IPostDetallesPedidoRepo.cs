using Data.Connection;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.PedidoCompleto.Domain.Interfaces.Repositories.PedidoCompleto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.PedidoCompleto
{
    public class PostDetallesPedidoRepo : IPostDetallesPedidoRepo
    {
        private readonly Conexion _conexion;

        public PostDetallesPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Inserta una línea de detalle (producto, cantidad, precio) asociada a un pedido existente.
        /// </summary>
        /// <param name="detalle">Entidad que contiene la información del detalle del pedido.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto detalle es nulo.</exception>
        /// <exception cref="ArgumentException">Se lanza si la cantidad es menor o igual a 0 o el precio es negativo.</exception>
        /// <exception cref="Exception">Se lanza ante errores de conexión o ejecución en la base de datos.</exception>
        public async Task InsertarDetallesPedido(Domain.Entities.DetallesPedido detalle)
        {
            
            if (detalle == null)
            {
                throw new ArgumentNullException(nameof(detalle), "La información del detalle del pedido no puede ser nula.");
            }

            if (detalle.IdPedido <= 0 || detalle.IdProducto <= 0)
            {
                throw new ArgumentException("El ID del pedido y el ID del producto deben ser válidos.", nameof(detalle));
            }

            if (detalle.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0.", nameof(detalle.Cantidad));
            }

            if (detalle.PrecioUnitario < 0)
            {
                throw new ArgumentException("El precio unitario no puede ser negativo.", nameof(detalle.PrecioUnitario));
            }

            string query = @"
                INSERT INTO DetallesPedido (IdPedido, IdProducto, Cantidad, PrecioUnitario)
                VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario)";

            try
            {
                
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

              
                using SqlCommand command = new SqlCommand(query, connection);

          
                command.Parameters.Add("@IdPedido", SqlDbType.Int).Value = detalle.IdPedido;
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = detalle.IdProducto;
                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al insertar el detalle del pedido {detalle.IdPedido}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar el detalle del pedido.", ex);
            }
        }
    }
}