using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.UseCase.Pedido;
using Domain.Interfaces.UseCase.PedidoCompleto;
using Domain.Interfaces.UseCases.Pedido;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IGetPedidoUseCase _pedidoUseCase;
        private readonly IPostPedidoUseCase _postPedidoUseCase;
        private readonly IActualizarEstadoPedidoUseCase _actualizarPedidoUseCase;
        private readonly IEliminarPedidoUseCase _eliminarPedidoUseCase;

        public PedidoController(IGetPedidoUseCase pedidoUseCase, IPostPedidoUseCase postPedidoUseCase,
            IActualizarEstadoPedidoUseCase actualizarPedidoUseCase, IEliminarPedidoUseCase eliminarPedidoUseCase)
        {
            _pedidoUseCase = pedidoUseCase;
            _actualizarPedidoUseCase = actualizarPedidoUseCase;
            _eliminarPedidoUseCase = eliminarPedidoUseCase;
            _postPedidoUseCase = postPedidoUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult result = NotFound();
            try { 
            List<PedidoConNombreProveedor> pedidos = await _pedidoUseCase.GetPedido();
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearPedidoRequest pedido)
        {
            try
            {
                PedidoConDetalles pedidoCompleto = await _postPedidoUseCase.CrearPedido(pedido);
                return Ok(pedidoCompleto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, int estado)
        {
            bool result = await _actualizarPedidoUseCase.ActualizarEstadoPedido(id, estado);
            return result ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _eliminarPedidoUseCase.eliminarPedido(id);
            return result ? Ok(result) : NotFound();
        }

    }
}
