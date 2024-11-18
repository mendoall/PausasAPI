
using Microsoft.AspNetCore.Mvc;
using MundoGluck.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using MundoGluck.Application.Empresas.Services;
namespace MundoGluck.Api.Controllers
{
    [Route("v1/api/empresas")]
    [ApiController]
    //[Authorize]
    public class EmpresaController(IEmpresaService service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<Empresa>> getEmpresas()
        {
            var empresas = await service.GetAllItems();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> getEmpresasById(int id)
        {
            var empresas = await service.GetByID(id);
            return Ok(empresas);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmpresa([FromBody] Empresa item)
        {
            try
            {
                var id = await service.AddItem(item);
                return CreatedAtAction(nameof(getEmpresasById), new { id }, null);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> ModifyEmpresa([FromBody] Empresa item)
        {
            await service.ModifyItem(item);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmpresa(int id)
        {
            await service.DeleteItem(id);
            return Ok();
        }

    }
}
