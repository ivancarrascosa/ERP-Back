using Data.Connection;
using Domain.Entities; // Asumiendo que Proveedor está aquí
using Domain.Interfaces.Repositories.Proveedor;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.Proveedor
{
    public class GetProveedorRepo : IGetProveedorRepo
    {
        private readonly Conexion _conexion;

        public GetProveedorRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Obtiene el listado completo de todos los proveedores registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Realiza una lectura de solo lectura (fast-forward) sobre la tabla Proveedor.
        /// Mapea los valores nulos de la base de datos a nulos o cadenas vacías según corresponda.
        /// </remarks>
        /// <returns>
        /// Una lista de objetos <see cref="Domain.Entities.Proveedor"/>. 
        /// Si no hay registros, devuelve una lista vacía.
        /// </returns>
        /// <exception cref="SqlException">Se lanza si ocurre un error de conexión o ejecución en la base de datos.</exception>
        public async Task<List<Domain.Entities.Proveedor>> GetProveedorRepositorio()
        {
            var proveedores = new List<Domain.Entities.Proveedor>();

            try
            {
                using var connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                string query = "SELECT IdProveedor, NombreEmpresa, Telefono, Email, Direccion FROM Proveedor";

                using var command = new SqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

               
                // Obtenemos los índices de las columnas fuera del bucle.
               
                int idxId = reader.GetOrdinal("IdProveedor");
                int idxNombre = reader.GetOrdinal("NombreEmpresa");
                int idxTel = reader.GetOrdinal("Telefono");
                int idxEmail = reader.GetOrdinal("Email");
                int idxDir = reader.GetOrdinal("Direccion");

                while (await reader.ReadAsync())
                {
                   
                    // Usamos IsDBNull para evitar excepciones si el campo está vacío en SQL.
                    

                    var proveedor = new Domain.Entities.Proveedor(
                        reader.GetInt32(idxId),
                        reader.GetString(idxNombre), // Asumimos que NombreEmpresa es NOT NULL en BD
                        reader.IsDBNull(idxTel) ? null : reader.GetString(idxTel),
                        reader.IsDBNull(idxEmail) ? null : reader.GetString(idxEmail),
                        reader.IsDBNull(idxDir) ? null : reader.GetString(idxDir)
                    );

                    proveedores.Add(proveedor);
                }

                return proveedores;
            }
            catch (SqlException)
            {
            
                throw;
            }
        }
    }
}