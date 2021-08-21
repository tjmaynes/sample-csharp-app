using System;
using ShoppingService.Core.Common;
using LanguageExt;

namespace ShoppingService.Core.Cart {
    public interface ICartService {
        EitherAsync<ServiceError, PagedResult<CartItem>> GetItemsFromCart(
            int currentPage = 0, int pageSize = 40
        );
        EitherAsync<ServiceError, CartItem> GetItemById(Guid id);
        EitherAsync<ServiceError, CartItem> AddItemToCart(CartItem newItem);
        EitherAsync<ServiceError, CartItem> UpdateItemInCart(CartItem updatedItem);
        EitherAsync<ServiceError, Guid> RemoveItemFromCart(Guid id);
    }
}
