using Domain.Entities;
using Domain.Interfaces.UseCases.Proveedor;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
		private readonly IGetProveedorUseCase _getProveedorUseCase;
		 
		public ProveedorController(IGetProveedorUseCase getProveedorUseCase)
		{
			this._getProveedorUseCase = getProveedorUseCase;
		}


		// GET: api/<ProveedorController>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			IActionResult response = NotFound();
			List<Proveedor> Proveedor = await _getProveedorUseCase.GetProveedores();
			if (Proveedor != null)
			{
				response = Ok(Proveedor);
			}
			return response;
		}

    }
}
