using Domain.DTOs;
using Domain.Entities;
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
		public PedidoController(IGetPedidoUseCase pedidoUseCase)
		{
			this._pedidoUseCase = pedidoUseCase;
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
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
