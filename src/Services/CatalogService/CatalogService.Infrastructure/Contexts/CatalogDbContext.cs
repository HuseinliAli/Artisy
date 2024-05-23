using CatalogService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Contexts
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<CatalogArtist> CatalogArtists { get; set; }
        public DbSet<CatalogArtistNationality> CatalogArtistNationalities { get; set; }
        public DbSet<CatalogColorPalette> CatalogColorPalettes { get; set; }
        public DbSet<CatalogGenre> CatalogGenres { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogItemCatalogColorPalette> CatalogItemCatalogColorPalettes { get; set; }
        public DbSet<CatalogMedium> CatalogMediums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
