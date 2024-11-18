using Microsoft.AspNetCore.Identity;
using MundoGluck.Infrastructure.Data;
using MundoGluck.Domain.Constants;





namespace MundoGluck.Infrastructure.Seeders;
internal class RolSeeder(MundoGluck_DbContext dbContext) : IRolSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private List<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = new()
        {
            new (UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper()
            },
            new (UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper()
            }
        };
        return roles;
    }
}
