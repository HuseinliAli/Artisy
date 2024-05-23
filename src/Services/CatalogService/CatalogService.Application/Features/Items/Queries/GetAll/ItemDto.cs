using CatalogService.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Features.Items.Queries.GetAll
{
    public record ItemDto(int Id, string Name, string Interval, decimal Price, string PictureUrl, string ArtistName, string ArtistNationality,string GenreName,string MediumName);
}
