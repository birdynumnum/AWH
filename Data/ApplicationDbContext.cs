using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AWH", throwIfV1Schema: false)
        {

        }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Kid> Kids { get; set; }
        public virtual DbSet<Present> Presents { get; set; }

        public virtual DbSet<LogAction> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
