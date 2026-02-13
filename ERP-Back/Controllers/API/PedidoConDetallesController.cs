using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP_Back.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoConDetallesController : ControllerBase
    {
        // GET: api/<PedidoConDetallesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PedidoConDetallesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    
    }
}
