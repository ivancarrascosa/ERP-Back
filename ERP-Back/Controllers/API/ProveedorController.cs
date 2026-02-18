using Domain.Entities;
using Domain.Interfaces.UseCases.Proveedor;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProveedorController : ControllerBase
    {
        private readonly IGetProveedorUseCase _getProveedorUseCase;

        public ProveedorController(IGetProveedorUseCase getProveedorUseCase)
        {
            this._getProveedorUseCase = getProveedorUseCase;
        }

        /// <summary>
        /// Obtiene el listado completo de proveedores registrados en el sistema.
        /// </summary>
        /// <returns>Una lista de objetos Proveedor.</returns>
        /// <response code="200">Retorna la lista de proveedores con éxito.</response>
        /// <response code="404">Si no se encuentran proveedores registrados.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Proveedor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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