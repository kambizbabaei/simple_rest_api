using Microsoft.EntityFrameworkCore;
using webapitest.Domains;

namespace webapitest.DataContext
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}