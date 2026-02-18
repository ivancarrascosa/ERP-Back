using Data.Connection;
using Domain.DTOs;
using System.Data;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Microsoft.Data.SqlClient;

namespace Data.Repositories.PedidoCompleto
{
    public class GetPedidoCompletoRepo : IGetPedidoCompletoRepo
    {
        private readonly Conexion _conexion;

        public GetPedidoCompletoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<PedidoConDetalles?> GetPedidoCompletoRepositorio(int idPedido)
        {
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();


            // PASO 1: Obtener la cabecera del pedido (Pedido + Proveedor + Usuario)

            string queryCabecera = @"
                SELECT p.IdPedido, p.FechaPedido, 
                       pr.NombreEmpresa AS NombreProveedor, 
                       pr.Telefono AS TelefonoProveedor,
                       pr.Email AS EmailProveedor,
                       p.TotalPedido, p.Estado,
                       u.Nombre AS NombreUsuario, 
                       u.Email AS EmailUsuario
                FROM Pedido p
                INNER JOIN Proveedor pr ON p.IdProveedor = pr.IdProveedor
                INNER JOIN Usuario u ON p.IdUsuario = u.FirebaseUID
                WHERE p.IdPedido = @IdPedido";

            using var cmdCabecera = new SqlCommand(queryCabecera, connection);
            cmdCabecera.Parameters.AddWithValue("@IdPedido", idPedido);

            using var readerCabecera = await cmdCabecera.ExecuteReaderAsync();

            // Si no hay resultado, el pedido no existe
            if (!await readerCabecera.ReadAsync())
                return null;

            // Guardamos los datos 
            // Nota: Uso ordinales o nombres directos. Asegúrate que en BD los tipos coincidan.
            int id = readerCabecera.GetInt32(readerCabecera.GetOrdinal("IdPedido"));
            DateTime fechaPedido = readerCabecera.GetDateTime(readerCabecera.GetOrdinal("FechaPedido"));
            string nombreProveedor = readerCabecera.GetString(readerCabecera.GetOrdinal("NombreProveedor"));
            string telefonoProveedor = readerCabecera.GetString(readerCabecera.GetOrdinal("TelefonoProveedor"));
            string emailProveedor = readerCabecera.GetString(readerCabecera.GetOrdinal("EmailProveedor"));
            decimal totalPedido = readerCabecera.GetDecimal(readerCabecera.GetOrdinal("TotalPedido"));
            // Conversión segura para el estado
            int estado = Convert.ToInt32(readerCabecera["Estado"]);
            string nombreUsuario = readerCabecera.GetString(readerCabecera.GetOrdinal("NombreUsuario"));
            string emailUsuario = readerCabecera.GetString(readerCabecera.GetOrdinal("EmailUsuario"));

            // Cerramos el primer reader
            await readerCabecera.CloseAsync();


            // PASO 2: Obtener las lineas de detalle de DetallePedido + Producto

            // CORRECCIÓN AQUÍ: Se cambió 'Borrado' por 'Eliminado'
            string queryDetalles = @"
                SELECT prod.Nombre AS NombreProducto, dp.Cantidad, dp.PrecioUnitario 
                FROM DetallesPedido dp 
                INNER JOIN Producto prod ON dp.IdProducto = prod.IdProducto 
                WHERE dp.IdPedido = @IdPedido AND dp.Eliminado = 0";

            using var cmdDetalles = new SqlCommand(queryDetalles, connection);
            cmdDetalles.Parameters.AddWithValue("@IdPedido", idPedido);

            using var readerDetalles = await cmdDetalles.ExecuteReaderAsync();

            // Lista de lineas de detalle con el nombre del producto
            var detalles = new List<DetallesPedidoConNombreProducto>();

            while (await readerDetalles.ReadAsync())
            {
                detalles.Add(new DetallesPedidoConNombreProducto(
                    readerDetalles.GetString(readerDetalles.GetOrdinal("NombreProducto")),
                    readerDetalles.GetInt32(readerDetalles.GetOrdinal("Cantidad")),
                    readerDetalles.GetDecimal(readerDetalles.GetOrdinal("PrecioUnitario"))
                ));
            }


            // PASO 3: Construir y devolver el DTO completo
            return new PedidoConDetalles(
                id, fechaPedido, nombreProveedor, telefonoProveedor,
                emailProveedor, totalPedido, estado, nombreUsuario,
                emailUsuario, detalles
            );
        }
    }
}