
﻿/CompositionRoot
/Data
    /Connection
        Conexion.cs
    /Repositories
/Pedido
    - GetPedidoRepo : IGetPedidoRepo (inyecta Conexion)
    - GetPedidoByIdRepo : IGetPedidoByIdRepo (inyecta Conexion)
    - PostPedidoRepo : IPostPedidoRepo (inyecta Conexion)
    - EditPedidoRepo : IEditPedidoRepo (inyecta Conexion)
    - DeletePedidoRepo : IDeletePedidoRepo (inyecta Conexion)
    - GetDetallesByPedidoRepo : IGetDetallesByPedidoRepo (inyecta Conexion)
    - PostDetallesRepo : IPostDetallesRepo (inyecta Conexion)


/Usuario
    - GetUsuarioRepo : IGetUsuarioRepo (inyecta Conexion)


/Producto
    - GetProductoRepo : IGetProductoRepo (inyecta Conexion)
    - GetProductoById : IGetProductoById (inyecta Conexion)
    - PostProductoRepo : IPostProductoRepo (inyecta Conexion)
    - EditProductoRepo : IEditProductoRepo (inyecta Conexion)
    - DeleteProductoRepo : IDeleteProductoRepo (inyecta Conexion)






/Proveedor
    - GetProveedorRepo : IGetProveedorRepo (inyecta Conexion)
    - GetProveedorByIdRepo : IGetProveedorByIdRepo (inyecta Conexion)
    - GetProveedorName : IGetProveedorName (inyecta Conexion)
    - GetProveedorTelefono : IGetProveedorTelefono (inyecta Conexion)
    - PostProveedorRepo : IPostProveedorRepo (inyecta Conexion)
    - EditProveedorRepo : IEditProveedorRepo (inyecta Conexion)
    - DeleteProveedorRepo : IDeleteProveedorRepo (inyecta Conexion)
/Domain
    /Dto
        __PedidoCompleto.cs__
        PedidoNombreProveedor(Inculirlo en pedido Completo, lo deberia hacer el front)
        ProductoNombreProveedor.cs
/Entities
    Usuario 
    * email: string
    * firebaseUID: string
    * nombre :string
    Pedido.cs
    * idPedido :int
    * fechaPedido :DateTime
    * idProveedor :int
    * totalPedido :decimal
    * estado : int
    * idUsuario :int
    DetallesPedido
    * idPedido :int
    * idProducto :int
    * cantidad :int
    * precioUnitario :decimal
    Producto.cs
    * idProducto :int
    * nombre :string
    * descripcion :string
    * precioCoste :decimal
    * stockActual :int
    * idProveedor :int
    Proveedor.cs
    * idProveedor :int
    * nombreEmpresa :string
    * telefono :string
    * email :string
    * direccion :string
/DTOs
        PedidoConDetalles.cs 
*  idPedido :int
*  fechaPedido :DateTime
* nombreProveedor :string
* telefonoProveedor: string
* emailProveedor: string
* totalPedido :decimal
* estado :int
* nombreUsuario :int
* emailUsuario: string
* detallesPedido: List<DetallesPedidoConNombreProducto>
        DetallesPedidoConNombreProducto.cs
* nombreProducto: String
* cantidad: int
* precioUnitario: decimal
        pedidoConNombreProveedor.cs 
* idPedido: int
* fechaPedido: DateTime
* nombreProveedor: string
* estado: int
* totalPedido: decimal
/UseCase
/Pedido
    - GetPedidoUseCase : IGetPedidoUseCase (inyecta IGetPedidoRepo)
    - GetPedidoByIdUseCase : IGetPedidoByIdUseCase (inyecta IGetPedidoByIdRepo)
    - PostPedidoUseCase : IPostPedidoUseCase (inyecta IPostPedidoRepo)
    - EditPedidoUseCase : IEditPedidoUseCase (inyecta IEditPedidoRepo)
    - DeletePedidoUseCase : IDeletePedidoUseCase (inyecta IDeletePedidoRepo)
    - GetDetallesByPedidoUseCase : IGetDetallesByPedidoUseCase (inyecta IGetDetallesByPedidoRepo)
        - ActualizarEstadoPedidoUseCase(int estadoPedido): IActualizarEstoPedidoUseCase 
    - PostDetallesUseCase : IPostDetallesUseCase (inyecta IPostDetallesRepo)


/Usuario
    - GetUsuarioUseCase : IGetUsuarioUseCase (inyecta IGetUsuarioRepo)


/Producto
    - GetProductoUseCase : IGetProductoUseCase (inyecta IGetProductoRepo)
    - GetProductoByIdUseCase : IGetProductoByIdUseCase (inyecta IGetProductoByIdRepo)
    - PostProductoUseCase : IPostProductoUseCase (inyecta IPostProductoRepo)
    - EditProductoUseCase : IEditProductoUseCase (inyecta IEditProductoRepo)
    - DeleteProductoUseCase : IDeleteProductoUseCase (inyecta IDeleteProductoRepo)


/Proveedor
    - GetProveedorUseCase : IGetProveedorUseCase (inyecta IGetProveedorRepo)
    - GetProveedorByIdUseCase : IGetProveedorByIdUseCase (inyecta IGetProveedorByIdRepo)
    - GetProveedorNameUseCase : IGetProveedorNameUseCase (inyecta IGetProveedorNameRepo)
    - GetProveedorTelefonoUseCase : IGetProveedorTelefonoUseCase (inyecta IGetProveedorTelefonoRepo)
    - PostProveedorUseCase : IPostProveedorUseCase (inyecta IPostProveedorRepo)
    - EditProveedorUseCase : IEditProveedorUseCase (inyecta IEditProveedorRepo)
    - DeleteProveedorUseCase : IDeleteProveedorUseCase (inyecta IDeleteProveedorRepo)


/Interfaces
/Pedido
    -IGetPedidoUseCase.cs +execute()
    -IGetPedidoByIdUseCase.cs +execute(int id)
    -IPostPedidoUseCase.cs +execute(Pedido pedido)
    -IEditPedidoUseCase.cs +execute(int id, Pedido pedido)
    -IDeletePedidoUseCase.cs +execute(int id)
    -IGetDetallesByPedidoUseCase.cs +execute(int idPedido)
    -IPostDetallesUseCase.cs +execute(DetallesPedido detalle)


/Usuario
    -IGetUsuarioUseCase.cs +execute(string firebaseUID)


/Producto
    -IGetProductoUseCase.cs +execute()
    -IGetProductoByIdUseCase.cs +execute(int id)
    -IPostProductoUseCase.cs +execute(Producto producto)
    -IEditProductoUseCase.cs +execute(int id, Producto producto)
    -IDeleteProductoUseCase.cs +execute(int id)


/Proveedor
    -IGetProveedorUseCase.cs +execute()
    -IGetProveedorByIdUseCase.cs +execute(int id)
    -IGetProveedorNameUseCase.cs +execute(int id)
    -IGetProveedorTelefonoUseCase.cs +execute(int id)
    -IPostProveedorUseCase.cs +execute(Proveedores proveedor)
    -IEditProveedorUseCase.cs +execute(int id, Proveedores proveedor)
    -IDeleteProveedorUseCase.cs +execute(int id)
/Repositories
    /Pedido
        -IGetPedidoRepo.cs +execute()
        -IGetPedidoByIdRepo.cs +execute(int id)
        -IPostPedidoRepo.cs +execute(Pedido pedido)
        -IEditPedidoRepo.cs +execute(int id, Pedido pedido)
        -IDeletePedidoRepo.cs +execute(int id)
        -IGetDetallesByPedidoRepo.cs +execute(int idPedido)
        -IPostDetallesRepo.cs +execute(DetallesPedido detalle)


    /Usuario
        -IGetUsuarioRepo.cs +execute(string firebaseUID)


    /Producto
        -IGetProductoRepo.cs +execute()
        -IGetProductoById.cs +execute(int id)
        -IPostProductoRepo.cs +execute(Producto producto)
        -IEditProductoRepo.cs +execute(int id, Producto producto)
        -IDeleteProductoRepo.cs +execute(int id)


    /Proveedor
        -IGetProveedorRepo.cs +execute()
        -IGetProveedorByIdRepo.cs +execute(int id)
- IGetProveedorName.cs +execute(int id)
- IGetProveedorTelefono,cs +execute(int id)
        -IPostProveedorRepo.cs +execute(Proveedores proveedor)
        -IEditProveedorRepo.cs +execute(int id, Proveedores proveedor)
        -IDeleteProveedorRepo.cs +execute(int id)
/Presentation
       /API
No lo hecho yo El usecase hay que cambiarlo 
        - UsuariosController.cs
            * Inyecta: IDefaultUseCase
            * [HttpGet] GetPerfil(string firebaseUID)
        - PedidosController.cs
            * Inyecta: IDefaultUseCase
            * [HttpGet] GetPedidos()
            * [HttpPost] CrearPedido(Pedido pedido)
            * [HttpPut] ActualizarEstado(int id, string estado)
        - DetallesPedidoController.cs
            * Inyecta: IDefaultUseCase
            * [HttpGet] GetDetallesByPedido(int idPedido)
            * [HttpPost] AgregarLineaDetalle(DetallesPedido detalle)
        - ProductosController.cs
            * Inyecta: IDefaultUseCase
            * [HttpGet] GetProductos()
            * [HttpPut] UpdateStock(int id, int cantidad)
        - ProveedoresController.cs
            * Inyecta: IDefaultUseCase
            * [HttpGet] GetProveedores()
        - PedidoCompletoController.cs (DTO Controller)
            * Inyecta: IDefaultUseCase
            * [HttpGet] GetFacturaCompleta(int idPedido) 
              (Retorna el DTO PedidoCompleto.cs que integra Pedido + Detalles + Productos)
