using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadastroClienteAPI.Domain;
using CadastroClienteAPI.Repository.Interfaces;

namespace CadastroClienteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienterepo;

        public ClienteController(IClienteRepository clienterepo)
        {
            _clienterepo = clienterepo;
        }

        // GET: api/Cliente
        [HttpGet]
        public List<Cliente> GetClientes()
        {
            return _clienterepo.GetClientes();
        }

        // GET: api/Cliente/1
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente([FromRoute] int id)
        {
            var cliente = _clienterepo.GetCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("{id}")]
        public IActionResult PutCliente(int clienteId, [FromBody] Cliente cliente)
        {
            if (cliente == null || clienteId != cliente.Id)
                return BadRequest(ModelState);

            if (!_clienterepo.UpdateCliente(cliente))
            {
                ModelState.AddModelError("", $"Something went wrong while updating cliente : {cliente.Nome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest(ModelState);
            if (_clienterepo.ClienteExists(cliente.Cpf))
            {
                ModelState.AddModelError("", "Cliente already Exist");
                return StatusCode(500, ModelState);
            }

            if (!_clienterepo.CreateCliente(cliente))
            {
                ModelState.AddModelError("", $"Something went wrong while saving movie record of {cliente.Nome}");
                return StatusCode(500, ModelState);
            }

            return Ok(cliente);
        }

        // DELETE: api/Cliente/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente([FromRoute] int id)
        {
            if (!_clienterepo.ClienteExists(id))
            {
                return NotFound();
            }

            var cliente = _clienterepo.GetCliente(id);

            if (!_clienterepo.DeleteCliente(cliente))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting movie : {cliente.Nome}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
