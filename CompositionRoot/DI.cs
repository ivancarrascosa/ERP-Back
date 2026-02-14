using Data.Repositories;
using Data.Repositories.Pedido;
using Data.Repositories.PedidoCompleto;
using Data.Repositories.Producto;
using Data.Repositories.Proveedor;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Pedido;
using Domain.Interfaces.Repositories.PedidoCompleto;
using Domain.Interfaces.Repositories.Producto;
using Domain.Interfaces.Repositories.Proveedor;
using Domain.Interfaces.UseCases.Pedido;
using Domain.Interfaces.UseCases.Producto;
using Domain.Interfaces.UseCases.Proveedor;
using Domain.UseCase;
using Domain.UseCases.Pedido;
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

			

			// Registrar casos de uso
			services.AddScoped<IGetProductoUseCase, GetProductoUseCase>();
			services.AddScoped<IGetProveedorUseCase, GetProveedorUseCase>();
			services.AddScoped<IGetPedidoUseCase, GetPedidoUseCase>();

			return services;
		}
	}
}