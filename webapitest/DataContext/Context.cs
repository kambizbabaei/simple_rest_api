using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapitest.Domains;

namespace webapitest.DataContext
{
    public class Context:IdentityDbContext<User, Role, int>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}