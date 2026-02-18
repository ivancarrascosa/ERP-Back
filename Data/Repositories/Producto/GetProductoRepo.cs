using Data.Connection;
using Domain.Interfaces.Repositories.Producto;
using Microsoft.Data.SqlClient;
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

        /// <summary>
        /// Obtiene el listado completo de productos disponibles en la base de datos.
        /// </summary>
        /// <returns>Una lista de objetos de la entidad Producto.</returns>
        /// <exception cref="Exception">Se lanza si ocurre un error de conexión o lectura de datos.</exception>
        public async Task<List<Domain.Entities.Producto>> GetProductoRepositorio()
        {
           
            List<Domain.Entities.Producto> productos = new List<Domain.Entities.Producto>();

     
            string query = "SELECT IdProducto, Nombre, Descripcion, PrecioCoste, StockActual, IdProveedor FROM Producto";

            try
            {
            
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

               
                using SqlCommand command = new SqlCommand(query, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                  

                    Domain.Entities.Producto entidadProducto = new Domain.Entities.Producto(
                        Convert.ToInt32(reader["IdProducto"]),
                        reader["Nombre"].ToString(),
                        reader["Descripcion"].ToString(),
                        Convert.ToDecimal(reader["PrecioCoste"]),
                        Convert.ToInt32(reader["StockActual"]),
                        Convert.ToInt32(reader["IdProveedor"])
                    );

                    productos.Add(entidadProducto);
                }

                return productos;
            }
            catch (SqlException ex)
            {
                // Manejo de errores específicos de SQL Server
                throw new Exception($"Error de base de datos al obtener el listado de productos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                throw new Exception("Error inesperado al recuperar los productos.", ex);
            }
        }
    }
}