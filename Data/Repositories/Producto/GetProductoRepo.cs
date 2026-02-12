// Repositorio: obtiene todos los productos de la tabla Productos
// Devuelve List<Producto> (entidad de dominio)
using Data.Connection;
using Domain.Interfaces.Repositories.Producto;
using MySql.Data.MySqlClient;
using System.Data;
namespace Data.Repositories.Producto
{
    public class GetProductoRepo : IGetProductoRepo
    {
        private readonly Conexion _conexion;

        public GetProductoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Domain.Entities.Producto>> GetProductoRepositorio()
        {
            var productos = new List<Domain.Entities.Producto>();

            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // SELECT de todos los campos de la tabla Productos
            string query = "SELECT IdProducto, Nombre, Descripcion, PrecioCoste, StockActual, IdProveedor FROM Productos";

            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            // Mapeamos cada fila a la entidad Producto
            while (await reader.ReadAsync())
            {
                productos.Add(new Domain.Entities.Producto(
                    reader.GetInt32("IdProducto"),
                    reader.GetString("Nombre"),
                    reader.GetString("Descripcion"),
                    reader.GetDecimal("PrecioCoste"),
                    reader.GetInt32("StockActual"),
                    reader.GetInt32("IdProveedor")
                ));
            }

            return productos;
        }

        
    }
}
