public interface IGetUsuario
{
    Task<string?> GetNombreByUidAsync(string uid);
}