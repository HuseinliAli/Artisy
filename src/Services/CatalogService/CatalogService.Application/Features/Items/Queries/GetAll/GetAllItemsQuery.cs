using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetAll
{
    public class GetAllItemsQuery : IRequest<List<ItemDto>>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int[]? ArtistIds { get; set; }
        public int[]? MediumIds { get; set; }
        public int[]? GenreIds { get; set; }
        public int[]? NationalityIds { get; set; }
    }
}
