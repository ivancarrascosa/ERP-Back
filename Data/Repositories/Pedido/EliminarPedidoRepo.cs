using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using MySql.Data.MySqlClient;
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
                SET Borrado = TRUE 
                WHERE IdPedido = @IdPedido";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdPedido", IdPedido);

            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
