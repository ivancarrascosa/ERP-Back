// Repositorio: obtiene todos los proveedores de la tabla Proveedor
// Devuelve List<Proveedor> (entidad de dominio)
using Data.Connection;
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

        public async Task<List<Domain.Entities.Proveedor>> GetProveedorRepositorio()
        {
            var proveedores = new List<Domain.Entities.Proveedor>();

            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // SELECT de todos los campos de Proveedor
            string query = "SELECT IdProveedor, NombreEmpresa, Telefono, Email, Direccion FROM Proveedor";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                proveedores.Add(new Domain.Entities.Proveedor(
                    reader.GetInt32("IdProveedor"),
                    reader.GetString("NombreEmpresa"),
                    reader.GetString("Telefono"),
                    reader.GetString("Email"),
                    reader.GetString("Direccion")
                ));
            }

            return proveedores;
        }
    }
}
