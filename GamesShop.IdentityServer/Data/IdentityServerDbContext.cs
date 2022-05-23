using GamesShop.IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamesShop.IdentityServer.Data
{
    public class IdentityServerDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options)
            : base(options)
        {
        }
    }
}
