using Data.Connection;
using Domain.Interfaces.Repositories.Producto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.Producto
{
    public class GetProductoByIdProveedorRepo : IGetProductoByIdProveedorRepo
    {
        private readonly Conexion _conexion;

        public GetProductoByIdProveedorRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Obtiene una lista de productos asociados a un proveedor específico.
        /// </summary>
        /// <param name="idProveedor">El identificador único del proveedor.</param>
        /// <returns>Una lista de objetos de tipo Producto.</returns>
        /// <exception cref="ArgumentException">Se lanza si el idProveedor es menor o igual a 0.</exception>
        /// <exception cref="Exception">Se lanza ante errores de conexión o lectura de la base de datos.</exception>
        public async Task<List<Domain.Entities.Producto>> getProductoByIdProveedor(int idProveedor)
        {
           
            if (idProveedor <= 0)
            {
                throw new ArgumentException("El identificador del proveedor no es válido.", nameof(idProveedor));
            }

       
            List<Domain.Entities.Producto> productos = new List<Domain.Entities.Producto>();

            string query = "SELECT IdProducto, Nombre, Descripcion, PrecioCoste, StockActual, IdProveedor FROM Producto WHERE IdProveedor = @IdProveedor";

            try
            {
                
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

             
                using SqlCommand command = new SqlCommand(query, connection);
     
                command.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = idProveedor;

               
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                // Mapeamos cada fila a la entidad Producto
                while (await reader.ReadAsync())
                {
                 

                    Domain.Entities.Producto producto = new Domain.Entities.Producto(
                        Convert.ToInt32(reader["IdProducto"]),
                        reader["Nombre"].ToString(),
                        reader["Descripcion"].ToString(),
                        Convert.ToDecimal(reader["PrecioCoste"]),
                        Convert.ToInt32(reader["StockActual"]),
                        Convert.ToInt32(reader["IdProveedor"])
                    );

                    productos.Add(producto);
                }

                return productos;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al obtener productos del proveedor {idProveedor}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al recuperar los productos.", ex);
            }
        }
    }
}