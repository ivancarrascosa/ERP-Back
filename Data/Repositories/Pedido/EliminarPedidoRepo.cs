using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Pedido
{
    public class EliminarPedidoRepo : IEliminarPedidoRepo
    {
        private readonly Conexion _conexion;

        public EliminarPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }
        public async Task<bool> eliminarPedido(int IdPedido)
        {
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            string query = @"
                UPDATE Pedidos 
                SET Borrado = 1 
                WHERE IdPedido = @IdPedido";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdPedido", IdPedido);

            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
