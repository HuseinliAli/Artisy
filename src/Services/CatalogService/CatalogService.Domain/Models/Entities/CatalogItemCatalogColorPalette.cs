using CatalogService.Domain.Models.Entities.Bases;

namespace CatalogService.Domain.Models.Entities
{
    public class CatalogItemCatalogColorPalette : BaseEntity<int>
    {
        public int CatalogItemId { get; set; }
        public CatalogItem CatalogItem { get; set; }

        public int CatalogColorPaletteId { get; set; }
        public CatalogColorPalette CatalogColorPalette { get; set; }
    }
}
