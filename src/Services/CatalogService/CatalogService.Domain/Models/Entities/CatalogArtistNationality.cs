using CatalogService.Domain.Models.Entities.Bases;

namespace CatalogService.Domain.Models.Entities
{
    public class CatalogArtistNationality : BaseEntity<int>
    {
        public string Nationality { get; set; }

        public List<CatalogArtist> CatalogArtists { get; set; }
    }
}
