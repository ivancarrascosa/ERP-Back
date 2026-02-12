// Repositorio: obtiene la factura completa de un pedido
// Hace un JOIN multiple entre las tablas:
//   Pedidos + Proveedores + Usuarios + DetallesPedido + Productos
// Devuelve el DTO PedidoConDetalles (cabecera + lista de lineas de detalle)
using Data.Connection;
using Domain.DTOs;
using System.Data;
using Domain.Interfaces.Repositories.PedidoCompleto;
using MySql.Data.MySqlClient;

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
                FROM Pedidos p
                INNER JOIN Proveedores pr ON p.IdProveedor = pr.IdProveedor
                INNER JOIN Usuarios u ON p.FirebaseUID = u.FirebaseUID
                WHERE p.IdPedido = @IdPedido";

            using var cmdCabecera = new MySqlCommand(queryCabecera, connection);
            cmdCabecera.Parameters.AddWithValue("@IdPedido", idPedido);
            using var readerCabecera = await cmdCabecera.ExecuteReaderAsync();

            // Si no hay resultado, el pedido no existe
            if (!await readerCabecera.ReadAsync())
                return null;

            // Guardamos los datos 
            int id = readerCabecera.GetInt32("IdPedido");
            DateTime fechaPedido = readerCabecera.GetDateTime("FechaPedido");
            string nombreProveedor = readerCabecera.GetString("NombreProveedor");
            string telefonoProveedor = readerCabecera.GetString("TelefonoProveedor");
            string emailProveedor = readerCabecera.GetString("EmailProveedor");
            decimal totalPedido = readerCabecera.GetDecimal("TotalPedido");
            int estado = readerCabecera.GetInt32("Estado");
            string nombreUsuario = readerCabecera.GetString("NombreUsuario");
            string emailUsuario = readerCabecera.GetString("EmailUsuario");

            // Cerramos el primer reader
            await readerCabecera.CloseAsync();

       
            //Obtener las lineas de detalle de DetallesPedido + Productos
            
            string queryDetalles = @"
                SELECT prod.Nombre AS NombreProducto, 
                       dp.Cantidad, 
                       dp.PrecioUnitario
                FROM DetallesPedido dp
                INNER JOIN Productos prod ON dp.IdProducto = prod.IdProducto
                WHERE dp.IdPedido = @IdPedido";

            using var cmdDetalles = new MySqlCommand(queryDetalles, connection);
            cmdDetalles.Parameters.AddWithValue("@IdPedido", idPedido);
            using var readerDetalles = await cmdDetalles.ExecuteReaderAsync();

            // Lista de lineas de detalle con el nombre del producto
            var detalles = new List<DetallesPedidoConNombreProducto>();

            while (await readerDetalles.ReadAsync())
            {
                detalles.Add(new DetallesPedidoConNombreProducto(
                    readerDetalles.GetString("NombreProducto"),
                    readerDetalles.GetInt32("Cantidad"),
                    readerDetalles.GetDecimal("PrecioUnitario")
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
