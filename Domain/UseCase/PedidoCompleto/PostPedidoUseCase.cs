using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.PedidoCompleto.Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.Producto;
using Domain.Interfaces.UseCase.PedidoCompleto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Pedido
{
    public class PostPedidoUseCase : IPostPedidoUseCase
    {
        private readonly IPostPedidoRepo _postPedidoRepo;
        private readonly IPostDetallesPedidoRepo _postDetallesRepo;
        private readonly IGetProductoByIdProveedorRepo _getProductoRepo;
        private readonly IGetPedidoCompletoRepo _getPedidoCompletoRepo;

        public PostPedidoUseCase(
            IPostPedidoRepo postPedidoRepo,
            IPostDetallesPedidoRepo postDetallesRepo,
            IGetProductoByIdProveedorRepo getProductoRepo,
            IGetPedidoCompletoRepo getPedidoCompletoRepo)
        {
            _postPedidoRepo = postPedidoRepo;
            _postDetallesRepo = postDetallesRepo;
            _getProductoRepo = getProductoRepo;
            _getPedidoCompletoRepo = getPedidoCompletoRepo;
        }

        public async Task<PedidoConDetalles> CrearPedido(CrearPedidoRequest request)
        {
            var productos = await _getProductoRepo.getProductoByIdProveedor(request.IdProveedor);

            decimal totalPedido = 0;
            var detallesConPrecio = new List<(int IdProducto, int Cantidad, decimal PrecioUnitario)>();

            foreach (var linea in request.Lineas)
            {
                var producto = productos.FirstOrDefault(p => p.IdProducto == linea.IdProducto);
                if (producto == null)
                    throw new Exception($"El producto con Id {linea.IdProducto} no pertenece al proveedor {request.IdProveedor}.");

                decimal precioUnitario = producto.PrecioCoste;
                totalPedido += precioUnitario * linea.Cantidad;
                detallesConPrecio.Add((linea.IdProducto, linea.Cantidad, precioUnitario));
            }

            var pedido = new Entities.Pedido(
                0, DateTime.Now, request.IdProveedor, totalPedido, 0, request.FirebaseUID
            );

            int idPedido = await _postPedidoRepo.CrearPedidoRepositorio(pedido);

            foreach (var detalle in detallesConPrecio)
            {
                var detalleEntity = new DetallesPedido(
                    idPedido, detalle.IdProducto, detalle.Cantidad, detalle.PrecioUnitario
                );
                await _postDetallesRepo.InsertarDetallesPedido(detalleEntity);
            }

            // Recuperar el pedido completo con todos los JOINs para devolver al front
            var pedidoCompleto = await _getPedidoCompletoRepo.GetPedidoCompletoRepositorio(idPedido);

            if (pedidoCompleto == null)
                throw new Exception("Error al recuperar el pedido recién creado.");

            return pedidoCompleto;
        }
    }
}
