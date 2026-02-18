using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.UseCase.Pedido;
using Domain.Interfaces.UseCase.PedidoCompleto;
using Domain.Interfaces.UseCases.Pedido;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
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

        /// <summary>
        /// Obtiene un listado de todos los pedidos con el nombre de su proveedor correspondiente.
        /// </summary>
        /// <returns>Lista de pedidos con información del proveedor.</returns>
        /// <response code="200">Retorna la lista de pedidos con éxito.</response>
        /// <response code="400">Si ocurre un error al intentar recuperar los datos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PedidoConNombreProveedor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<PedidoConNombreProveedor> pedidos = await _pedidoUseCase.GetPedido();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo pedido en el sistema.
        /// </summary>
        /// <param name="pedido">Objeto con los datos necesarios para crear el pedido.</param>
        /// <returns>El pedido recién creado con sus detalles.</returns>
        /// <response code="200">Pedido creado exitosamente.</response>
        /// <response code="400">Si los datos enviados no son válidos o el proceso falla.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PedidoConDetalles), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Actualiza el estado de un pedido existente.
        /// </summary>
        /// <param name="id">ID del pedido a actualizar.</param>
        /// <param name="estado">Nuevo estado a asignar.</param>
        /// <returns>Resultado booleano de la operación.</returns>
        /// <response code="200">Estado actualizado correctamente.</response>
        /// <response code="404">Si el pedido no existe.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, int estado)
        {
            bool result = await _actualizarPedidoUseCase.ActualizarEstadoPedido(id, estado);
            return result ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Elimina un pedido del sistema mediante su identificador.
        /// </summary>
        /// <param name="id">ID del pedido a eliminar.</param>
        /// <returns>Resultado booleano de la operación.</returns>
        /// <response code="200">Pedido eliminado correctamente.</response>
        /// <response code="404">Si el pedido no existe.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _eliminarPedidoUseCase.eliminarPedido(id);
            return result ? Ok(result) : NotFound();
        }
    }
}