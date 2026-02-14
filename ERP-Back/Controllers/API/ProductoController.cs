using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Domain.Interfaces.UseCases.Producto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
		private readonly IGetProductoUseCase _getProductoUseCase;
		public ProductoController(IGetProductoUseCase getProductoUseCase)
		{
			this._getProductoUseCase = getProductoUseCase;
		}


		// GET: api/<ProductoController>
		[HttpGet]
        public IActionResult Get()
		{
			IActionResult response = BadRequest();
			Task<List<Producto>> producto = _getProductoUseCase.GetProductos();
			if (producto != null)
			{
				response = Ok(producto);
			}
			return response;
		}

    }
}
