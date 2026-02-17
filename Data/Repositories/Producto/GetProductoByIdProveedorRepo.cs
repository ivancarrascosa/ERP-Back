using Data.Connection;
using Domain.Interfaces.Repositories.Producto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Producto
{
    public class GetProductoByIdProveedorRepo : IGetProductoByIdProveedorRepo
    {
        private readonly Conexion _conexion;

        public GetProductoByIdProveedorRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Domain.Entities.Producto>> getProductoByIdProveedor(int idProveedor)
        {
            var productos = new List<Domain.Entities.Producto>();

            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // SELECT de todos los campos de la tabla Producto
            string query = "SELECT IdProducto, Nombre, Descripcion, PrecioCoste, StockActual, IdProveedor FROM Producto WHERE IdProveedor = @IdProveedor";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdProveedor", idProveedor);
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
