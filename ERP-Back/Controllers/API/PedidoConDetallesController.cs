using Domain.DTOs;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // Especifica que la API siempre retorna JSON
    public class PedidoConDetallesController : ControllerBase
    {
        private readonly IGetPedidoCompletoUseCase _pedidoCompletoUseCase;

        public PedidoConDetallesController(IGetPedidoCompletoUseCase pedidoCompletoUseCase)
        {
            this._pedidoCompletoUseCase = pedidoCompletoUseCase;
        }

        /// <summary>
        /// Obtiene un pedido completo incluyendo todos sus detalles mediante su ID.
        /// </summary>
        /// <param name="id">Identificador único del pedido.</param>
        /// <returns>Los datos del pedido con sus detalles.</returns>
        /// <response code="200">Retorna el pedido solicitado.</response>
        /// <response code="404">Si no se encuentra el pedido con el ID proporcionado.</response>
        /// <response code="400">Si ocurre un error durante el procesamiento de la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PedidoConDetalles), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            IActionResult response = NotFound();
            try
            {
                PedidoConDetalles pedidoCompleto = await _pedidoCompletoUseCase.GetPedidoCompleto(id);
                if (pedidoCompleto != null)
                {
                    response = Ok(pedidoCompleto);
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(new { message = ex.Message });
            }

            return response;
        }
    }
}