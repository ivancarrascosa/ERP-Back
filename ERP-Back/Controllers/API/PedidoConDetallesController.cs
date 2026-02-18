using Domain.DTOs;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoConDetallesController : ControllerBase
    {
        private readonly IGetPedidoCompletoUseCase _pedidoCompletoUseCase;
        public PedidoConDetallesController(IGetPedidoCompletoUseCase pedidoCompletoUseCase)
        {
            this._pedidoCompletoUseCase = pedidoCompletoUseCase;
        }

     

        // GET api/<PedidoConDetallesController>/5
        [HttpGet("{id}")]
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
                response = BadRequest(ex);
            }
            
            return response;
            
		}

    
    }
}
