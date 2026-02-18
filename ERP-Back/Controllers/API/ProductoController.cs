using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.UseCase.Producto;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Domain.Interfaces.UseCases.Producto;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductoController : ControllerBase
    {
        private readonly IGetProductoUseCase _getProductoUseCase;
        private readonly IGetProductoByIdProveedorUseCase _getProductoByIdProveedorUseCase;

        public ProductoController(IGetProductoUseCase getProductoUseCase, IGetProductoByIdProveedorUseCase getProductoByIdProveedorUseCase)
        {
            this._getProductoUseCase = getProductoUseCase;
            this._getProductoByIdProveedorUseCase = getProductoByIdProveedorUseCase;
        }

        /// <summary>
        /// Obtiene el catálogo completo de productos registrados.
        /// </summary>
        /// <returns>Una lista de todos los productos.</returns>
        /// <response code="200">Retorna la lista de productos encontrada.</response>
        /// <response code="404">Si no existen productos en la base de datos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IActionResult response = NotFound();
            List<Producto> producto = await _getProductoUseCase.GetProductos();
            if (producto != null)
            {
                response = Ok(producto);
            }
            return response;
        }

        /// <summary>
        /// Obtiene la lista de productos asociados a un proveedor específico.
        /// </summary>
        /// <param name="id">ID del proveedor para filtrar los productos.</param>
        /// <returns>Lista de productos suministrados por el proveedor indicado.</returns>
        /// <response code="200">Retorna la lista de productos del proveedor.</response>
        /// <response code="404">Si no se encuentran productos para el ID de proveedor proporcionado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            IActionResult response = NotFound();
            List<Producto> lista = await _getProductoByIdProveedorUseCase.getProductoByIdProveedor(id);
            if (lista != null)
            {
                response = Ok(lista);
            }
            return response;
        }
    }
}