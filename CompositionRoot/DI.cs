using Data.Repositories;
using Data.Repositories.Pedido;
using Data.Repositories.PedidoCompleto;
using Data.Repositories.Producto;
using Data.Repositories.Proveedor;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.PedidoCompleto.Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.Producto;
using Domain.Interfaces.Repositories.Proveedor;
using Domain.Interfaces.UseCase.Pedido;
using Domain.Interfaces.UseCase.Producto;
using Domain.Interfaces.UseCases.Pedido;
using Domain.Interfaces.UseCases.PedidoCompleto;
using Domain.Interfaces.UseCases.Producto;
using Domain.Interfaces.UseCases.Proveedor;
using Domain.UseCase;
using Domain.UseCase.Pedido;
using Domain.UseCase.Producto;
using Domain.UseCases.Pedido;
using Domain.UseCases.PedidoCompleto;
using Domain.UseCases.Producto;
using Domain.UseCases.Proveedor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Container
{
	public static class DI
	{
		//Instalar el paquete NuGet Microsoft.Extensions.Configuration.Abstractions 
		//y el paquete Microsoft.Extensions.DependencyInjection.Abstractions
		public static IServiceCollection AddContainer(this IServiceCollection services, IConfiguration configuration)
		{
			// Registrar repositorios concretos
			services.AddScoped<IGetPedidoRepo, GetPedidoRepo>();
			services.AddScoped<IActualizarEstadoPedidoRepo, ActualizarEstadoPedidoRepo>();
			services.AddScoped<IPostPedidoRepo, PostPedidoRepo>();
			services.AddScoped<IEliminarPedidoRepo, EliminarPedidoRepo>();

			services.AddScoped<IGetPedidoCompletoRepo, GetPedidoCompletoRepo>();

			services.AddScoped<IGetProductoRepo, GetProductoRepo>();
			services.AddScoped<IUpdateStockProductoRepo, UpdateStockProductoRepo>();

			services.AddScoped<IGetProveedorRepo, GetProveedorRepo>();
			services.AddScoped<IGetProductoByIdProveedorRepo, GetProductoByIdProveedorRepo>();
            services.AddScoped<IPostDetallesPedidoRepo, PostDetallesPedidoRepo>();

            // Registrar casos de uso
            services.AddScoped<IGetPedidoUseCase, GetPedidoUseCase>();
            services.AddScoped<IGetPedidoCompletoRepo, GetPedidoCompletoRepo>();
            services.AddScoped<IEliminarPedidoUseCase, EliminarPedidoUseCase>();
			services.AddScoped<IActualizarEstadoPedidoUseCase, ActualizarEstadoPedidoUseCase>();

			services.AddScoped<IGetPedidoCompletoUseCase, GetPedidoCompletoUseCase>();

			services.AddScoped<IGetProductoUseCase, GetProductoUseCase>();
			services.AddScoped<IUpdateStockProductoUseCase, UpdateStockProductoUseCase>();

			services.AddScoped<IGetProveedorUseCase, GetProveedorUseCase>();
			services.AddScoped<IGetProductoByIdProveedorUseCase, GetProductoByIdProveedorUseCase>();

            return services;
		}
	}
}