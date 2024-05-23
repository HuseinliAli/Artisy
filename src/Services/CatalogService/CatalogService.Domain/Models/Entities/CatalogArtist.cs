using CatalogService.Domain.Models.Entities.Bases;

namespace CatalogService.Domain.Models.Entities
{
    public class CatalogArtist : BaseEntity<int>
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public int CatalogArtistNationalityId { get; set; }
        public CatalogArtistNationality CatalogArtistNationality { get; set; }

        public List<CatalogItem> CatalogItems { get; set; }
    }
}
