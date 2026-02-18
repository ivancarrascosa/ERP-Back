using Data.Connection;
using Domain.DTOs;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Repositories.PedidoCompleto
{
    public class GetPedidoCompletoRepo : IGetPedidoCompletoRepo
    {
        private readonly Conexion _conexion;

        public GetPedidoCompletoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Obtiene la información completa de un pedido, incluyendo cabecera (datos del pedido, proveedor, usuario) 
        /// y sus líneas de detalle (productos).
        /// </summary>
        /// <param name="idPedido">El identificador del pedido a recuperar.</param>
        /// <returns>
        /// Un objeto <see cref="PedidoConDetalles"/> con toda la información si existe; 
        /// devuelve <c>null</c> si el pedido no se encuentra.
        /// </returns>
        /// <exception cref="ArgumentException">Se lanza si el idPedido es menor o igual a 0.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error de conexión o lectura.</exception>
        public async Task<PedidoConDetalles?> GetPedidoCompletoRepositorio(int idPedido)
        {
            // Precondiciones
            if (idPedido <= 0)
            {
                throw new ArgumentException("El identificador del pedido no es válido.", nameof(idPedido));
            }

            // Queries separadas para mayor claridad
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

            string queryDetalles = @"
                SELECT prod.Nombre AS NombreProducto, dp.Cantidad, dp.PrecioUnitario 
                FROM DetallesPedido dp 
                INNER JOIN Pedido p ON dp.IdPedido = p.IdPedido
                INNER JOIN Producto prod ON dp.IdProducto = prod.IdProducto 
                WHERE dp.IdPedido = @IdPedido AND p.Borrado = 0";

            try
            {
                using SqlConnection connection = _conexion.ObtenerConexion();
                await connection.OpenAsync();

                // ---------------------------------------------------------
                // PASO 1: Obtener la cabecera
                // ---------------------------------------------------------

                // Variables para almacenar temporalmente los datos de la cabecera
                int id;
                DateTime fechaPedido;
                string nombreProveedor;
                string telefonoProveedor;
                string emailProveedor;
                decimal totalPedido;
                int estado;
                string nombreUsuario;
                string emailUsuario;

                using (SqlCommand cmdCabecera = new SqlCommand(queryCabecera, connection))
                {
                    cmdCabecera.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;

                    using (SqlDataReader readerCabecera = await cmdCabecera.ExecuteReaderAsync())
                    {
                        if (!await readerCabecera.ReadAsync())
                        {
                            return null; 
                        }

                        // Mapeo de datos usando los nombres de columna exactos
                        id = Convert.ToInt32(readerCabecera["IdPedido"]);
                        fechaPedido = Convert.ToDateTime(readerCabecera["FechaPedido"]);
                        nombreProveedor = readerCabecera["NombreProveedor"].ToString();
                        telefonoProveedor = readerCabecera["TelefonoProveedor"].ToString();
                        emailProveedor = readerCabecera["EmailProveedor"].ToString();
                        totalPedido = Convert.ToDecimal(readerCabecera["TotalPedido"]);
                        estado = Convert.ToInt32(readerCabecera["Estado"]);
                        nombreUsuario = readerCabecera["NombreUsuario"].ToString();
                        emailUsuario = readerCabecera["EmailUsuario"].ToString();
                    }
                   
                }

              
                // Obtener los detalles
                

                List<DetallesPedidoConNombreProducto> detalles = new List<DetallesPedidoConNombreProducto>();

                using (SqlCommand cmdDetalles = new SqlCommand(queryDetalles, connection))
                {
                    cmdDetalles.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;

                    using (SqlDataReader readerDetalles = await cmdDetalles.ExecuteReaderAsync())
                    {
                        while (await readerDetalles.ReadAsync())
                        {
                            // Instanciamos el DTO de detalle
                          
                            DetallesPedidoConNombreProducto detalleItem = new DetallesPedidoConNombreProducto(
                                readerDetalles["NombreProducto"].ToString(),
                                Convert.ToInt32(readerDetalles["Cantidad"]),
                                Convert.ToDecimal(readerDetalles["PrecioUnitario"])
                            );

                            detalles.Add(detalleItem);
                        }
                    }
                }

               
                //  Construir DTO final
                

                return new PedidoConDetalles(
                    id,
                    fechaPedido,
                    nombreProveedor,
                    telefonoProveedor,
                    emailProveedor,
                    totalPedido,
                    estado,
                    nombreUsuario,
                    emailUsuario,
                    detalles
                );
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al recuperar el pedido completo {idPedido}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al recuperar el pedido completo.", ex);
            }
        }
    }
}