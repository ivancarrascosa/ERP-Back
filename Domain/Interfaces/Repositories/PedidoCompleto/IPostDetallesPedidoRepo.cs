using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.PedidoCompleto
{
    namespace Domain.Interfaces.Repositories.PedidoCompleto
    {
        public interface IPostDetallesPedidoRepo
        {
            Task InsertarDetallesPedido(Entities.DetallesPedido detalle);
        }
    }

}
