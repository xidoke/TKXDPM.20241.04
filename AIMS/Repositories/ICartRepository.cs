﻿using AIMS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetCartItemsAsync(string email);
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int id);
        Task DeleteCartItemsByEmailAsync(string email);
        Task<bool> UpdateSelectedStatusAsync(int id, bool isSelected);
    }
}