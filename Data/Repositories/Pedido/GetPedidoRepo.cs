// Repositorio: obtiene todos los pedidos con el nombre del proveedor
// Hace un JOIN entre la tabla Pedido y Proveedor
// Devuelve List<PedidoConNombreProveedor> (DTO)
using Data.Connection;
using Domain.DTOs;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.Pedido
{
    public class GetPedidoRepo : IGetPedidoRepo
    {
        // Inyeccion de la clase Conexion para acceder a la BBDD
        private readonly Conexion _conexion;

        public GetPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<PedidoConNombreProveedor>> GetPedidoRepositorio()
        {
            var pedidos = new List<PedidoConNombreProveedor>();

            // Obtenemos la conexion a SQL Server
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // Query con JOIN para traer el nombre del proveedor junto al pedido
            string query = @"
                SELECT p.IdPedido, p.FechaPedido, pr.NombreEmpresa AS NombreProveedor, 
                       p.Estado, p.TotalPedido
                FROM Pedido p
                INNER JOIN Proveedor pr ON p.IdProveedor = pr.IdProveedor
                ORDER BY p.FechaPedido DESC";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            // Leemos cada fila y creamos el DTO
            while (await reader.ReadAsync())
            {
                pedidos.Add(new PedidoConNombreProveedor(
                    reader.GetInt32("IdPedido"),
                    reader.GetDateTime("FechaPedido"),
                    reader.GetString("NombreProveedor"),
                    Convert.ToInt32(reader["Estado"]),
                    reader.GetDecimal("TotalPedido")
                ));
            }

            return pedidos;
        }
    }
}
