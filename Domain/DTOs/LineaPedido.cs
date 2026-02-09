using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LineaPedido
    {
        public int IdProducto { get; }
        public int Cantidad { get; }

        public LineaPedido(int idProducto, int cantidad)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
        }
    }
}
