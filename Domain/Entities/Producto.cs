using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        public int IdProducto { get; }
        public string Nombre { get; }
        public string Descripcion { get; }
        public decimal PrecioCoste { get; }
        public int StockActual { get; }
        public int IdProveedor { get; }

        public Producto(int idProducto, string nombre, string descripcion,
            decimal precioCoste, int stockActual, int idProveedor)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioCoste = precioCoste;
            StockActual = stockActual;
            IdProveedor = idProveedor;
        }
    }
}
