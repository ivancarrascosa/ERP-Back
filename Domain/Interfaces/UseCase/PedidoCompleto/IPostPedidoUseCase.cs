using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.UseCase.PedidoCompleto
{
    public interface IPostPedidoUseCase
    {
        Task<PedidoConDetalles> CrearPedido(CrearPedidoRequest request);
    }
}
