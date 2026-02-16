using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.UseCase.Producto;
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
        private readonly IGetProductoByIdProveedorUseCase _getProductoByIdProveedorUseCase;
		public ProductoController(IGetProductoUseCase getProductoUseCase, IGetProductoByIdProveedorUseCase getProductoByIdProveedorUseCase)
		{
			this._getProductoUseCase = getProductoUseCase;
            this._getProductoByIdProveedorUseCase = getProductoByIdProveedorUseCase;
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
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult response = BadRequest();
            Task<List<Producto>> lista = _getProductoByIdProveedorUseCase.getProductoByIdProveedor(id);
            if (lista != null)
            {
                response = Ok(lista);
            }
            return response;
        }


    }
}
