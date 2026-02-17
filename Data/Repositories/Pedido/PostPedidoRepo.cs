// Repositorio: inserta un nuevo pedido en la tabla Pedido
// Usa parametros para evitar SQL Injection
// Devuelve el ID autogenerado del pedido insertado (LAST_INSERT_ID)
using Data.Connection;
using Domain.Interfaces.Repositories.Pedido;
using Microsoft.Data.SqlClient;


namespace Data.Repositories.Pedido
{
    public class PostPedidoRepo : IPostPedidoRepo
    {
        private readonly Conexion _conexion;

        public PostPedidoRepo(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<int> CrearPedidoRepositorio(Domain.Entities.Pedido pedido)
        {
            using var connection = _conexion.ObtenerConexion();
            await connection.OpenAsync();

            // INSERT del pedido con parametros (@param) para evitar SQL Injection
            string query = @"
                INSERT INTO Pedido (FechaPedido, IdProveedor, TotalPedido, Estado, FirebaseUID)
                VALUES (@FechaPedido, @IdProveedor, @TotalPedido, @Estado, @FirebaseUID);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var command = new SqlCommand(query, connection);

            // Asignamos los valores de la entidad Pedido a los parametros
            command.Parameters.AddWithValue("@FechaPedido", pedido.FechaPedido);
            command.Parameters.AddWithValue("@IdProveedor", pedido.IdProveedor);
            command.Parameters.AddWithValue("@TotalPedido", pedido.TotalPedido);
            command.Parameters.AddWithValue("@Estado", pedido.Estado);
            command.Parameters.AddWithValue("@FirebaseUID", pedido.FirebaseUID);

            // ExecuteScalar devuelve el primer valor de la primera fila (el ID generado)
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

      
    }
}
