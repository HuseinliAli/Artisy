using CatalogService.Domain.Models.Entities.Bases;

namespace CatalogService.Domain.Models.Entities
{
    public class CatalogColorPalette : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }

        public List<CatalogItemCatalogColorPalette> CatalogItemCatalogColorPalettes { get; set; }
    }
}
