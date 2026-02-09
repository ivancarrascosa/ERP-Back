using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PedidoConDetalles
    {
        public int IdPedido { get; }
        public DateTime FechaPedido { get; }
        public string NombreProveedor { get; }
        public string TelefonoProveedor { get; }
        public string EmailProveedor { get; }
        public decimal TotalPedido { get; }
        public int Estado { get; }
        public string NombreUsuario { get; }
        public string EmailUsuario { get; }
        public List<DetallesPedidoConNombreProducto> DetallesPedido { get; }

        public PedidoConDetalles(int idPedido, DateTime fechaPedido, string nombreProveedor,
            string telefonoProveedor, string emailProveedor, decimal totalPedido, int estado,
            string nombreUsuario, string emailUsuario, List<DetallesPedidoConNombreProducto> detallesPedido)
        {
            IdPedido = idPedido;
            FechaPedido = fechaPedido;
            NombreProveedor = nombreProveedor;
            TelefonoProveedor = telefonoProveedor;
            EmailProveedor = emailProveedor;
            TotalPedido = totalPedido;
            Estado = estado;
            NombreUsuario = nombreUsuario;
            EmailUsuario = emailUsuario;
            DetallesPedido = detallesPedido;
        }
    }
}
