using CatalogService.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Repositories
{
    public interface ICatalogArtistRepository : IGenericRepository<CatalogArtist,int>
    {
    }
}
