﻿using CatalogService.Domain.Models.Entities.Bases;

namespace CatalogService.Domain.Models.Entities
{
    public class CatalogGenre : BaseEntity<int>
    {
        public string Name { get; set; }

        public List<CatalogItem> CatalogItems { get; set; }
    }
}
