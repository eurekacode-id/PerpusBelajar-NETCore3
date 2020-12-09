using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerpusBelajar.Constant;
using PerpusBelajar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerpusBelajar.Models
{
    // Add the custom ApplicationUser class in IdentityDbContext to extend the IdentityUser base class
    public class PerpusBelajarDbContext : IdentityDbContext<ApplicationUser>
    {
        public PerpusBelajarDbContext(DbContextOptions<PerpusBelajarDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //example of seeding data 
            modelBuilder.Seed();

            // enforce NO ACTION ON DELETE behaviour
            foreach(var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Book> Books { get; set; }
    }
}
