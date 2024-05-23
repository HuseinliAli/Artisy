using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Domain.Models.Entities.Bases;

namespace CatalogService.Domain.Models.Entities
{
    public class CatalogItem:BaseEntity<int>
    {
        public string Name { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public int CatalogArtistId { get; set; }
        public CatalogArtist CatalogArtist { get; set; }

        public int CatalogGenreId { get; set; }
        public CatalogGenre CatalogGenre { get; set; }

        public int CatalogMediumId { get; set; }
        public CatalogMedium CatalogMedium { get; set; }

        public List<CatalogItemCatalogColorPalette> CatalogItemCatalogColorPalettes { get; set; }
    }
}
