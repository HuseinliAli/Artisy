using CatalogService.Application.Repositories;
using MediatR;

namespace CatalogService.Application.Features.Items.Queries.GetAll
{
    public class GetAllItemsQueryHandler(ICatalogItemRepository catalogItemRepository) : IRequestHandler<GetAllItemsQuery, List<ItemDto>>
    {
        public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var result = catalogItemRepository.GetAll();

            if (request.MinPrice == null)
                request.MinPrice = 0;

            if (request.MaxPrice == null)
                request.MaxPrice = catalogItemRepository.GetAll().OrderByDescending(i=>i.Price).FirstOrDefault().Price;

            result = result.Where(i => i.Price>=request.MinPrice && i.Price<=request.MaxPrice);

            if (request.ArtistIds != null && request.ArtistIds.Any())
                result = result.Where(i => request.ArtistIds.Contains(i.CatalogArtistId));

            if (request.MediumIds != null && request.MediumIds.Any())
                result = result.Where(i => request.MediumIds.Contains(i.CatalogMediumId));

            if (request.GenreIds != null && request.GenreIds.Any())
                result = result.Where(i => request.GenreIds.Contains(i.CatalogGenreId));

            if (request.NationalityIds != null && request.NationalityIds.Any())
                result = result.Where(i => request.NationalityIds.Contains(i.CatalogArtist.CatalogArtistNationalityId));

            return result.Select(i =>
                new ItemDto(
                    i.Id,
                    i.Name,
                    $"{i.StartedDate.Year.ToString()}-{i.FinishedDate.Year.ToString()}",
                    i.Price, i.PictureUrl,
                    i.CatalogArtist.FullName,
                    i.CatalogArtist.CatalogArtistNationality.Nationality,
                    i.CatalogGenre.Name, i.CatalogMedium.Name)).ToList();
        }
    }
}
