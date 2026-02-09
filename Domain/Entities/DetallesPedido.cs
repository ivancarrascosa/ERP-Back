using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetallesPedido
    {
        public int IdPedido { get; }
        public int IdProducto { get; }
        public int Cantidad { get; }
        public decimal PrecioUnitario { get; }

        public DetallesPedido(int idPedido, int idProducto, int cantidad, decimal precioUnitario)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
