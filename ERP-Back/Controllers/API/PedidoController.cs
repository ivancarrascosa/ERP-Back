using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.UseCase.Pedido;
using Domain.Interfaces.UseCases.Pedido;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

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
            IActualizarEstadoPedidoUseCase actualizarPedidoUseCase, IEliminarPedidoUseCase _eliminarPedidoUseCase)
		{
			this._pedidoUseCase = pedidoUseCase;
            this._actualizarPedidoUseCase = actualizarPedidoUseCase;
            this._eliminarPedidoUseCase = _eliminarPedidoUseCase;
            this._postPedidoUseCase = postPedidoUseCase;
		}


		// GET: api/<PedidoController>
		[HttpGet]
        public IActionResult Get()
        {
			IActionResult response = BadRequest();
            Task<List<PedidoConNombreProveedor>> pedido = _pedidoUseCase.GetPedido();
			if ( pedido != null)
			{
				response = Ok(pedido);
			}
			return response;
		}

       
        // POST api/<PedidoController>
        [HttpPost]
        public IActionResult Post(CrearPedidoRequest pedido)
        {
            _postPedidoUseCase.CrearPedido(new Pedido(1, new DateTime(), 1, 1, 1, ""));
            //Hay que formatear lo que recibe en el UseCase no en el controller
            //Como devuelve Task<int> hay que mirarselo
            return Ok(pedido);

        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, int estado)
        {
            IActionResult result = BadRequest();
            Task<bool> n = this._actualizarPedidoUseCase.ActualizarEstadoPedido(id, estado);
            if (n != null)
            {
                 result = Ok(n);
            }
            return result;
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			IActionResult result = BadRequest();
			Task<bool> n = this._eliminarPedidoUseCase.eliminarPedido(id);

			if (n != null)
			{
				result = Ok(n);
			}
			return result;
		}
    }
}
