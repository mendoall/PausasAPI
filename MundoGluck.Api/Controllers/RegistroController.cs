
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MundoGluck.Application.Usuarios;
using MundoGluck.Application.Usuarios.DTOs;
using MundoGluck.Application.Usuarios.Services;
using MundoGluck.Domain.Constants;
using MundoGluck.Domain.Entidades;
using System.Text;

namespace MundoGluck.Api.Controllers
{
    [Route("v1/api/registro")]
    [ApiController]
    [Authorize]
    public class RegistroController(IUsuarioService service, UserManager<Usuario> userManager, IUserContext userContext) : ControllerBase
    {

        [HttpGet("UsuarioGet")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Usuario>> GetUsuarios()
        {
            var usuarios = await service.GetAllItems();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuariosId(string id)
        {
            var usuario = await service.GetByID(id);
            return Ok(usuario);
        }

        //TODO Borrar y revisar los services y el repositorio.
        [HttpPost("RegistroAdd")]
        public async Task<ActionResult> AddUsuario([FromBody] Usuario item)
        {
            try
            {
                var id = await service.AddItem(item);
                return CreatedAtAction(nameof(GetUsuariosId), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint para la actualización de detalles del usuario
        [HttpPut("RegistroModificacion")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UsuarioDTO item)
        {
            await service.ModifyItem(item);
            return Ok("Detalles actualizados correctamente.");
        }

        [HttpDelete("RegistroDelete")]
        public async Task<ActionResult> DeleteUsuario(string id)
        {
            await service.DeleteItem(id);
            return Ok();
        }

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return BadRequest("Invalid Request.");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            
            //hay que usar el enconder porque el metodo resetPassword de ASP.NET Identity lo decodifica
           token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            
            request.ResetToken = token;

            var response = await service.ForgotPassword(request);

            if(response != System.Net.HttpStatusCode.Accepted)
            {
                return BadRequest("Invalid Request.");
            }
         
            return Ok();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserDTO request)
        {
            await service.assingUserRole(request);
            return Ok();

        }

        [HttpGet("currentUserInfo")]
        public ActionResult<CurrentUser> GetCurrentUserInfo()
        {
            var user = userContext.GetCurrentUser();
            return Ok(user);
        }

    }
}
