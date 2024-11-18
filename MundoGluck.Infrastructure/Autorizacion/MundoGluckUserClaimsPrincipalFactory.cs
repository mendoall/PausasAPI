using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MundoGluck.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Infrastructure.Autorizacion;
public class MundoGluckUserClaimsPrincipalFactory( UserManager<Usuario> userManager,
    RoleManager<IdentityRole> roleManager, 
    IOptions<IdentityOptions> options) 
        : UserClaimsPrincipalFactory<Usuario, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(Usuario user)
    {
        var id = await GenerateClaimsAsync(user);
        
        if(user.EmpresaId != null)
        {
            id.AddClaim(new Claim("EmpresaId", user.EmpresaId.ToString()));
        }

        if(user.Nombre != null)
        {
            id.AddClaim(new Claim("Nombre", user.Nombre));
        }


        return new ClaimsPrincipal(id);



    }
}

