using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonTCGWishlist.Areas.Identity.Data;

namespace PokemonTCGWishlist.Data
{
    public class PokemonTCGWishlistContext : IdentityDbContext<PokemonTCGWishlistUser>
    {
        public PokemonTCGWishlistContext(DbContextOptions<PokemonTCGWishlistContext> options)
            : base(options)
        {
        }

        public DbSet<PokemonTCGWishlistUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<PokemonTCGWishlistUser>(entity =>
            {
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                base.OnModelCreating(builder);
            });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
