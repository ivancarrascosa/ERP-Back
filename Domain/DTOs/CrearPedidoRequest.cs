using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CrearPedidoRequest
    {
        public int IdProveedor { get; }
        public string FirebaseUID { get; }
        public List<LineaPedido> Lineas { get; }

        public CrearPedidoRequest(int idProveedor, string firebaseUID, List<LineaPedido> lineas)
        {
            IdProveedor = idProveedor;
            FirebaseUID = firebaseUID;
            Lineas = lineas;
        }
    }
}
