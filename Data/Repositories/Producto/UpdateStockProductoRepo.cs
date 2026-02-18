using System.Data;
using Data.Connection;
using Domain.Interfaces.Repositories.Producto;
using Microsoft.Data.SqlClient;

namespace Data.Repositories.Producto
{
    public class UpdateStockProductoRepo : IUpdateStockProductoRepo
    {
        private readonly Conexion _conexion;

        public UpdateStockProductoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Actualiza únicamente el campo StockActual de un producto específico.
        /// </summary>
        /// <remarks>
        /// Este método sobrescribe el valor actual. Asegúrese de tener el valor final calculado antes de llamar a este método.
        /// </remarks>
        /// <param name="idProducto">El identificador único del producto. Debe ser mayor a 0.</param>
        /// <param name="cantidad">La nueva cantidad de stock a asignar. No puede ser negativa.</param>
        /// <returns>
        /// Retorna <c>true</c> si se encontró el registro y se actualizó correctamente; 
        /// de lo contrario retorna <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Se lanza si el <paramref name="idProducto"/> es menor o igual a 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si la <paramref name="cantidad"/> es negativa.</exception>
        public async Task<bool> ActualizarStockProductoRepo(int idProducto, int cantidad)
        {
   

            if (idProducto <= 0)
            {
                throw new ArgumentException("El ID del producto debe ser un entero positivo.", nameof(idProducto));
            }

            if (cantidad < 0)
            {
                // Asumimos que un ERP no permite stock negativo físico. 
             
                throw new ArgumentOutOfRangeException(nameof(cantidad), "El stock actual no puede ser negativo.");
            }

            
            try
            {
                using var connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                string query = @"
                    UPDATE Producto
                    SET StockActual = @Cantidad 
                    WHERE IdProducto = @IdProducto";

                using var command = new SqlCommand(query, connection);

                // Uso de SqlDbType para mayor rendimiento y seguridad
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;

                int filasAfectadas = await command.ExecuteNonQueryAsync();

                return filasAfectadas > 0;
            }
            catch (SqlException)
            {
               
                throw;
            }
        }
    }
}