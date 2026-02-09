using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class DetallesPedidoConNombreProducto
    {
        public string NombreProducto { get; }
        public int Cantidad { get; }
        public decimal PrecioUnitario { get; }

        public DetallesPedidoConNombreProducto(string nombreProducto, int cantidad, decimal precioUnitario)
        {
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
