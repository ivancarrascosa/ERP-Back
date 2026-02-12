using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.UseCase.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Pedido
{
    public class EliminarPedidoUseCase : IEliminarPedidoUseCase
    {
        // Repositorio inyectado por constructor
        private readonly IEliminarPedidoRepo _eliminarPedidoRepo;

        public EliminarPedidoUseCase(IEliminarPedidoRepo eliminarPedidoRepo)
        {
            _eliminarPedidoRepo = eliminarPedidoRepo;
        }
        public Task<bool> eliminarPedido(int idPedido)
        {
            _eliminarPedidoRepo.eliminarPedido(idPedido); 
            return Task.FromResult(true);
        }
    }
}
