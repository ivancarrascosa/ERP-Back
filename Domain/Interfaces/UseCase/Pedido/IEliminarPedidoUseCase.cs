using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.UseCase.Pedido
{
    public interface IEliminarPedidoUseCase
    {
        Task<bool> eliminarPedido(int idPedido);
    }
}
