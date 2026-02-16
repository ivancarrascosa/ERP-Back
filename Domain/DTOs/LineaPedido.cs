using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LineaPedido
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public LineaPedido() { }

        public LineaPedido(int idProducto, int cantidad)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
        }
    }
}
