﻿namespace AIMS.Data.Entities
{
    public class CartItem
    {
        public bool isSelected { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
