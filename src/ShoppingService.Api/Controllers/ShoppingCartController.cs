using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingService.Core.Cart;
using ShoppingService.Core.Common;
using static LanguageExt.Prelude;
using ShoppingService.Api.Extensions;

namespace ShoppingService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/shopping-cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        
        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> Get(int currentPage = 0, int pageSize = 40) =>
            await match(_service.GetItemsFromCart(currentPage, pageSize),
                Right: result => Ok(new Dictionary<string, PagedResult<CartItem>> {{ "data", result }}),
                Left: error => StatusCode(error.ErrorCode.ToStatusCode(), error.Message)
            );

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetById(Guid id) =>
            await match(_service.GetItemById(id),
                Right: item => Ok(new Dictionary<string, CartItem> {{ "data", item }}),
                Left: error => StatusCode(error.ErrorCode.ToStatusCode(), error.Message)
            );

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<CartItem>> Post([FromBody] CartItem newItem) =>
            await match(_service.AddItemToCart(newItem),
                Right: item => StatusCode(201, new Dictionary<string, CartItem> {{ "data", item }}),
                Left: error => StatusCode(error.ErrorCode.ToStatusCode(), error.Message)
            );

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<ActionResult<CartItem>> Put(string id, [FromBody] CartItem updatedItem) =>
            await match(_service.UpdateItemInCart(updatedItem),
                Right: item => Ok(new Dictionary<string, CartItem> {{ "data", item }}),
                Left: error => StatusCode(error.ErrorCode.ToStatusCode(), error.Message)
            );

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> Delete(Guid id) =>
            await match(_service.RemoveItemFromCart(id),
                Right: removedId => Ok(new Dictionary<string, Guid> {{ "data", id }}),
                Left: error => StatusCode(error.ErrorCode.ToStatusCode(), error.Message)
            );
    }
}