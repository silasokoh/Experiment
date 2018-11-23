using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Experiment.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class MyUser : IdentityUser
    {


        public virtual ICollection<Patient> Patients { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class GilgalbyteDbContext : IdentityDbContext<MyUser>
    {
        public GilgalbyteDbContext()
            : base("gilgalbyte_db", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckIn>().HasRequired(profile => profile.Schedule);
            modelBuilder.Entity<CheckOut>().HasRequired(profile => profile.Schedule);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public static GilgalbyteDbContext Create()
        {
            return new GilgalbyteDbContext();
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<CheckIn> CheckIns { get; set; }


        public DbSet<CheckOut> CheckOuts { get; set; }
    }
}