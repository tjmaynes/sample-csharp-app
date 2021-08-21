using Microsoft.EntityFrameworkCore;
using ShoppingService.Core.Cart;

namespace ShoppingService.Api {
    public class PostgresqlContext: DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
    }
}