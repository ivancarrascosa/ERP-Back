using Data.Connection;
using Microsoft.Data.SqlClient;

public class GetUsuario : IGetUsuario
{
    private readonly Conexion _conexion;

    public GetUsuario(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task<string?> GetNombreByUidAsync(string uid)
    {
        const string query = "SELECT nombre FROM Usuario WHERE FirebaseUID = @uid";

        await using var connection = _conexion.ObtenerConexion();

        if (connection.State != System.Data.ConnectionState.Open)
            await connection.OpenAsync();

        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@uid", uid);

        var result = await command.ExecuteScalarAsync();

        return result as string;
    }
}