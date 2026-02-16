using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CrearPedidoRequest
    {
        public int IdProveedor { get; set; }
        public string FirebaseUID { get; set; }
        public List<LineaPedido> Lineas { get; set; }

        public CrearPedidoRequest() { }

        public CrearPedidoRequest(int idProveedor, string firebaseUID, List<LineaPedido> lineas)
        {
            IdProveedor = idProveedor;
            FirebaseUID = firebaseUID;
            Lineas = lineas;
        }
    }
}
