using Microsoft.Extensions.Logging;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Repositorios;
using MundoGluck.Application.Usuarios.DTOs;
using MundoGluck.Application.Usuarios.Services;
using Microsoft.AspNetCore.Identity;
using MundoGluck.Domain.EmailSender;
using MundoGluck.Domain.Exceptions;
using System.Net;
using MundoGluck.Domain.Constants;



internal class UsuarioService(IUsuarioRepositorio usuarioRepositorio, 
    ILogger<UsuarioService> logger, 
    IEmailSender emailSender,
    RoleManager<IdentityRole> roleManager,
    UserManager<Usuario> userManager
    ) : IUsuarioService
{
   
    public async Task<int> AddItem(Usuario item)
    {
        var id = await usuarioRepositorio.AddItemAsync(item);
        return id;
    }

    //TODO que pasa con los claims y los roles?
    public async Task DeleteItem(string id)
    {
        await usuarioRepositorio.DeleteItemAsync(id);
    }

   
    public async Task<IEnumerable<Usuario>> GetAllItems()
    {
        logger.LogInformation("Obteniendo todos los usuarios");
        var usuarios = await usuarioRepositorio.GetAllAsync();
        return usuarios;
    }

    public Task<Usuario?> GetByID(string id)
    {
        return usuarioRepositorio.GetItemByIdAsync(id);
    }

    public async Task ModifyItem(UsuarioDTO item)
    {
        if (item == null || string.IsNullOrEmpty(item.Email))
        {
            logger.LogError("Email es requerido.");
            return;
        }

        var usuario = await usuarioRepositorio.FindByEmailAsync(item.Email);
        if (usuario == null)
        {
            logger.LogError("Usuario no encontrado.");
            return;
        }

        //TODO que es tipo usuario
        usuario.Nombre = item.Nombre;
        usuario.EmpresaId = item.EmpresaId;
        usuario.TipoUsuario = item.TipoUsuario;
        usuario.PhoneNumber = item.PhoneNumber;
        

        // EmailConfirmed se setea en true para que se pueda realizar el cambio de contraseña correctamente
        usuario.EmailConfirmed = true ;

        try
        {
            await usuarioRepositorio.ModifyItemAsync(usuario);
            
            //TODO Discutir es aqui donde se actualiza el usuario
            await userManager.AddToRoleAsync(usuario, UserRoles.User);
            logger.LogInformation("Detalles actualizados correctamente.");
        }
        catch (Exception ex)
        {
            logger.LogError("Errores al actualizar el usuario: {message}", ex.Message);
        }
    }
    public async Task<System.Net.HttpStatusCode> ForgotPassword(ForgotPasswordDTO emailDto)
    {
        try
        {
            var link = $"http://localhost:5173/ResetPasswordPage?token={emailDto.ResetToken}&email={emailDto.Email}";
            var emailBody = $@"
        <html>
        <body>
            <p>Hola,</p>
            <p>Hemos recibido una solicitud para restablecer tu contraseña. Por favor, haz clic en el enlace de abajo para restablecer tu contraseña:</p>
            <p><a href='{link}'>Restablecer contraseña</a></p>
            <p>Si no solicitaste un restablecimiento de contraseña, por favor ignora este correo electrónico.</p>
            <p>Gracias,</p>
            <p>El equipo de MundoGluck</p>
        </body>
        </html>";

            var response = await emailSender.SendEmailAsync("Recuperación de contraseña", emailBody, emailDto.Email);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("Error sending email: {message}", ex.Message);
            return HttpStatusCode.InternalServerError;
        }


    }

    public async Task assingUserRole(AssignUserDTO assignUserDto)
    {
        logger.LogInformation("Asignando rol al usuario: {@Request}", assignUserDto);

        var usuario = await usuarioRepositorio.FindByEmailAsync(assignUserDto.UserEmail)
            ?? throw new NotFoundException(nameof(Usuario), assignUserDto.UserEmail);

        var role = await roleManager.FindByNameAsync(assignUserDto.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), assignUserDto.RoleName);

        await userManager.AddToRoleAsync(usuario, role.Name!);
            
    }
}
