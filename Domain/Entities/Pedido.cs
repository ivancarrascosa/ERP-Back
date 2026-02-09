using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido
    {
        public int IdPedido { get; }
        public DateTime FechaPedido { get; }
        public int IdProveedor { get; }
        public decimal TotalPedido { get; }
        public int Estado { get; }
        public string FirebaseUID { get; }

        public Pedido(int idPedido, DateTime fechaPedido, int idProveedor,
            decimal totalPedido, int estado, string firebaseUID)
        {
            IdPedido = idPedido;
            FechaPedido = fechaPedido;
            IdProveedor = idProveedor;
            TotalPedido = totalPedido;
            Estado = estado;
            FirebaseUID = firebaseUID;
        }
    }
}
