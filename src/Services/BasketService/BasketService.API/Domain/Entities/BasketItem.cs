﻿using System.ComponentModel.DataAnnotations;

namespace BasketService.API.Domain.Entities
{
    public class BasketItem
    {
        public string Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal OldUnitPrice { get; set; }

        public string PictureUrl { get; set; }
    }
}
