using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Services.Interfaces;

namespace WebShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _service;
        private readonly ILogger<ShoppingCartItemController> _logger;

        public ShoppingCartItemController(IShoppingCartItemService service, ILogger<ShoppingCartItemController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os itens do carrinho que estão com o status do parametro
        /// </summary>
        /// <param name="shoppingCartId">ID do carrinho </param>
        /// <param name="active">true = produto esta Valido no carrinho , False = produto foi removido do carrinho</param>
        /// <returns></returns>
        [HttpGet("{CustomerId}")]
        public ActionResult<List<ShoppingCartItem>> Get(long shoppingCartId, bool active)
        {
            try
            {
                _logger.LogInformation("Received get request");

                return Ok(_service.GetShoppingCartItem(shoppingCartId,active));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Adiciona o produto no carrinho
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody] ShoppingCartItemViewModel item)
        {
            try
            {
                _logger.LogInformation("Received post request");

                if (ModelState.IsValid)
                {
                    _service.Add(item.ShoppingCartId, item.ProductId, item.ProductQuantity);

                    return Ok("success");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}