// Clase que encapsula la conexion a la base de datos SQL Server
// Se inyecta en todos los repositorios para obtener una conexion
// La cadena de conexion se lee desde appsettings.json via IConfiguration


using Microsoft.Data.SqlClient;

namespace Data.Connection
{
    public class Conexion
    {
        // Cadena de conexion leida desde la configuracion
        private readonly string _connectionString;

        // Recibe IConfiguration por DI para leer "ConnectionStrings:DefaultConnection"
        public Conexion(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Devuelve una nueva conexion SQL Server (el llamador debe abrirla y cerrarla)
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
