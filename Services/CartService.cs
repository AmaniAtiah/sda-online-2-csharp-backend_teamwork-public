using Backend.Dtos;
using Backend.Dtos.Pagination;
using Backend.EntityFramework;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Middlewares;


namespace Backend.Services
{
    public class CartService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CartService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }






// display product in cart 

        public async Task<CartDto> GetCartAsync(Guid cartId, Guid userId)
        {
            var cart = await _appDbContext.Carts
            .Include(c => c.CartProducts)
               .ThenInclude(cp => cp.Product)
           
               .FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);

            if (cart == null)
            {
                // Handle the case where the cart does not exist
                throw new Exception("Cart not found");
            }

            var cartDto = _mapper.Map<CartDto>(cart);
            

            return cartDto;
        }

// delete product from cart not delete cart

        public async Task<bool> DeleteProductFromCartAsync(Guid cartId, Guid productId, Guid userId)
        {
            var cart = await _appDbContext.Carts
               .Include(c => c.CartProducts)
               .FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);

            if (cart == null)
            {
                // Handle the case where the cart does not exist
                return false;
            }

            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);

            if (cartProduct == null)
            {
                // Handle the case where the product does not exist in the cart
                return false;
            }

            _appDbContext.CartProducts.Remove(cartProduct);
            await _appDbContext.SaveChangesAsync();

            return true;
        }





//   public async Task<Cart?> GetCartAsync(Guid cartId, Guid userId)
//         {
//             // return await _appDbContext.Users.Include(u => u.Profile).Include(u => u.Orders).FirstOrDefaultAsync(u => u.UserId == userId);
//             return await _appDbContext.Carts
//             .Include( c=> c.CartProducts)
//             .ThenInclude(cp => cp.Product)
//             // user
            
//             .FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);


//         }




public async Task<CartDto> CreateCartAndAddProductAsync(CreateCartProductDto cartProductDTO, Guid userId)
        {


            if (cartProductDTO == null)
                throw new ArgumentNullException(nameof(cartProductDTO));

            var cart = await GetOrCreateCartAsync(userId);
            await AddProductToCartAsync(cart.CartId, cartProductDTO.ProductId, cartProductDTO.Quantity);

            return cart;
        }

private async Task<CartDto> GetOrCreateCartAsync(Guid userId)
{
    var existingCart = await _appDbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

    if (existingCart != null){
        var existingCartDto = _mapper.Map<CartDto>(existingCart);
        return existingCartDto;
}

    var newCart = new Cart
    {
        CartId = Guid.NewGuid(),
        UserId = userId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };



    // mapper

    await _appDbContext.Carts.AddAsync(newCart);
    await _appDbContext.SaveChangesAsync();

    // var cartDto = new CartDto {
    //     CartId = newCart.CartId,
    //     UserId = newCart.UserId
    // };
    // return cartDto;

    var cartDto = _mapper.Map<CartDto>(newCart);
    return cartDto;


}
        private async Task<CartProductDto> AddProductToCartAsync(Guid cartId, Guid productId, int quantity)
        {
            var cartProduct = await _appDbContext.CartProducts.FindAsync(cartId, productId);
            if (cartProduct == null)
            {
                cartProduct = new CartProduct
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _appDbContext.CartProducts.AddAsync(cartProduct);
            }
            else
            {
                cartProduct.Quantity += quantity;
            }
            await _appDbContext.SaveChangesAsync();

            // var cartProductDTO = new CartProductDto{
            //     CartId = cartProduct.CartId,
            //     ProductId = cartProduct.ProductId,
            //     Quantity = cartProduct.Quantity,
            //     CreatedAt = cartProduct.CreatedAt,
            //     UpdatedAt = cartProduct.UpdatedAt,

            // };

            // mapper 
            var cartProductDto = _mapper.Map<CartProductDto>(cartProduct);
            return cartProductDto;
        }
    }

    }








   

