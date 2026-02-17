// Repositorio: actualiza unicamente el campo StockActual de un producto
// Recibe el id del producto y la nueva cantidad de stock
// Devuelve true si la actualizacion fue exitosa
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

        public async Task<bool> ActualizarStockProductoRepo(int idProducto, int cantidad)
        {
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // UPDATE solo del campo StockActual
            string query = @"
                UPDATE Producto
                SET StockActual = @Cantidad 
                WHERE IdProducto = @IdProducto";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Cantidad", cantidad);
            command.Parameters.AddWithValue("@IdProducto", idProducto);

            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
