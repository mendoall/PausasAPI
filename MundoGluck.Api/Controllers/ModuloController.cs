
using Microsoft.AspNetCore.Mvc;
using MundoGluck.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using MundoGluck.Application.Modulos.Services;
namespace MundoGluck.Api.Controllers
{
    [Route("v1/api/modulos")]
    [ApiController]
    //[Authorize]
    public class ModuloController(IModuloService service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<Modulo>> getModulos()
        {
            var modulos = await service.GetAllItems();
            return Ok(modulos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Modulo>> getModulosById(int id)
        {
            var modulos = await service.GetByID(id);
            return Ok(modulos);
        }

        [HttpPost]
        public async Task<ActionResult> AddModulo([FromBody] Modulo item)
        {
            try
            {
                var id = await service.AddItem(item);
                return CreatedAtAction(nameof(getModulosById), new { id }, null);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> ModifyModulo([FromBody] Modulo item)
        {
            await service.ModifyItem(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModulo([FromRoute] int id)
        {
            await service.DeleteItem(id);
            return Ok();
        }

    }
}
