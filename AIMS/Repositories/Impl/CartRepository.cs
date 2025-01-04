using AIMS.Data.Contexts;
using AIMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Repositories.Impl
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public CartRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string email)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            if (email != currentUserEmail)
            {
                return null;
            }

            // No changes needed here as we are getting all cart items for a specific email
            return await _context.CartItems.Where(c => c.Email == email).ToListAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(int mediaId)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            // Correctly find by MediaID and Email
            var cartItem = await _context.CartItems
                .Where(c => c.MediaID == mediaId && c.Email == currentUserEmail)
                .FirstOrDefaultAsync();

            return cartItem; // Return null if not found
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            cartItem.Email = currentUserEmail; // Set email on adding

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            // Find by MediaID and Email for update
            var existingCartItem = await _context.CartItems
                .Where(c => c.MediaID == cartItem.MediaID && c.Email == currentUserEmail)
                .FirstOrDefaultAsync();

            if (existingCartItem == null)
            {
                return null; // Or throw an exception
            }

            // Update properties
            existingCartItem.isSelected = cartItem.isSelected;
            // existingCartItem.MediaID = cartItem.MediaID;  // MediaID should not be updated
            existingCartItem.MediaName = cartItem.MediaName;
            existingCartItem.MediaImgUrl = cartItem.MediaImgUrl;
            existingCartItem.Price = cartItem.Price;
            existingCartItem.Quantity = cartItem.Quantity;
            existingCartItem.Status = cartItem.Status;

            // No need for this line as we are using FirstOrDefaultAsync()
            //_context.Entry(existingCartItem).State = EntityState.Modified; 

            await _context.SaveChangesAsync();
            return existingCartItem;
        }

        public async Task DeleteCartItemAsync(int mediaId)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            // Correctly find by MediaID and Email for deletion
            var cartItem = await _context.CartItems
                .Where(c => c.MediaID == mediaId && c.Email == currentUserEmail)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                return; // Or throw an exception
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItemsByEmailAsync(string email)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            if (email != currentUserEmail)
            {
                return; // Or throw an exception
            }

            // No changes needed here, we are deleting all cart items for a specific email
            var cartItems = await _context.CartItems.Where(c => c.Email == email).ToListAsync();
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSelectedStatusAsync(int mediaId, bool isSelected)
        {
            string currentUserEmail = _userRepository.GetCurrentUserEmail();
            // Correctly find by MediaID and Email for status update
            var cartItem = await _context.CartItems
                .Where(c => c.MediaID == mediaId && c.Email == currentUserEmail)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                return false; // Or throw an exception
            }

            cartItem.isSelected = isSelected;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}