using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PedidoConNombreProveedor
    {
        public int IdPedido { get; }
        public DateTime FechaPedido { get; }
        public string NombreProveedor { get; }
        public int Estado { get; }
        public decimal TotalPedido { get; }

        public PedidoConNombreProveedor(int idPedido, DateTime fechaPedido, string nombreProveedor,
            int estado, decimal totalPedido)
        {
            IdPedido = idPedido;
            FechaPedido = fechaPedido;
            NombreProveedor = nombreProveedor;
            Estado = estado;
            TotalPedido = totalPedido;
        }
    }
}
