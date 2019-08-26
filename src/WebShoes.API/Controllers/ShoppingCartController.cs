using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Interfaces;

namespace WebShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(IShoppingCartService service, ILogger<ShoppingCartController> logger)
        {
            _service = service;
            _logger = logger;
        }



        /// <summary>
        /// Busca o carrinho de compras ativo 
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <returns>carrinho de compra</returns>
        [HttpGet("{customerId}")]
        public ActionResult<ShoppingCart> Get(long customerId)
        {
            try
            {
                _logger.LogInformation("Received get request");

                return Ok(_service.GetActiveShoppingCart(customerId));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Cria o cabeçalho do carrinho de compras
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <returns></returns>
        [HttpPost("{customerId}")]
        public ActionResult<string> Post(long customerId)
        {
            try
            {
                _logger.LogInformation("Received post request");

                if (ModelState.IsValid)
                {
                    _service.Add(customerId);

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
        
        /// <summary>
        ///  Altera o status do carrinho de compra 
        /// </summary>
        /// <param name="shoppingCart">I</param> 
        /// <returns></returns>
        [HttpPatch]
        public ActionResult<string> Patch([FromBody] ShoppingCartViewModel shoppingCart)
        {
            try
            {
                _logger.LogInformation("Received get request");

                _service.UpdateStatus(shoppingCart.ShoppingCartId, shoppingCart.ShoppingCartStatus);

                return Ok("success");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}