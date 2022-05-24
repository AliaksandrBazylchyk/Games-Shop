using Microsoft.EntityFrameworkCore;

namespace GamesShop.UserManagement.DAL.Contexts
{
    public class UserManagementDBContext : DbContext
    {
        public UserManagementDBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }
    }
}