using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IGetUsuario _getUsuario;

    public UsuarioController(IGetUsuario getUsuario)
    {
        _getUsuario = getUsuario;
    }

    [HttpGet("{uid}")]
    public async Task<IActionResult> GetNombre(string uid)
    {
        if (string.IsNullOrWhiteSpace(uid))
            return BadRequest("El uid no puede estar vacío.");

        var nombre = await _getUsuario.GetNombreByUidAsync(uid);

        if (nombre is null)
            return NotFound($"No se encontró ningún usuario con el uid: {uid}");

        return Ok(new { nombre });
    }
}