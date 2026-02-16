using Data.Connection;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.PedidoCompleto.Domain.Interfaces.Repositories.PedidoCompleto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.PedidoCompleto
{
    public class PostDetallesPedidoRepo : IPostDetallesPedidoRepo
    {
        private readonly Conexion _conexion;

        public PostDetallesPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task InsertarDetallesPedido(Domain.Entities.DetallesPedido detalle)
        {
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            string query = @"
                INSERT INTO DetallesPedido (IdPedido, IdProducto, Cantidad, PrecioUnitario)
                VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdPedido", detalle.IdPedido);
            command.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
            command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
            command.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);

            await command.ExecuteNonQueryAsync();
        }
    }
}
