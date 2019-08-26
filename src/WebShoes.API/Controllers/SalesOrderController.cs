using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Services.Interfaces;

namespace WebShoes.API.Controllers
{
    /// <summary>
    /// Pedido
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderServices _salesOrderServices;
        private readonly ILogger<SalesOrderController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesOrderServices"></param>
        /// <param name="logger"></param>
        public SalesOrderController(ISalesOrderServices salesOrderServices, ILogger<SalesOrderController> logger)
        {
            _salesOrderServices = salesOrderServices;
            _logger = logger;
        }

        /// <summary>
        /// Retorna o Pedido pelo Id
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <returns></returns>
        [HttpGet("{salesOrderId}")]
        public ActionResult<SalesOrder> Get(long salesOrderId)
        {
            try
            {
                _logger.LogInformation("Received get request");
                return Ok(_salesOrderServices.GetById(salesOrderId));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        /// <summary>
        /// Retorna o Pedido pelo ShoppingCartId
        /// </summary>
        /// <param name="shoppingCartId"></param>
        /// <returns></returns>
        [HttpGet("shoppingCartId/{shoppingCartId}")]
        public ActionResult<SalesOrder> GetSalesOrderByShoppingCartId(long shoppingCartId)
        {
            try
            {
                _logger.LogInformation("Received get request");
                return Ok(_salesOrderServices.GetByShoppingCartId(shoppingCartId));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        /// <summary>
        /// Grava o Pedido
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpPost("{customerId}")]
        public ActionResult<string> Post([FromRoute] long customerId)
        {
            try
            {
                _logger.LogInformation("Received post request");
                return Ok(_salesOrderServices.Add(customerId));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return BadRequest($"Erro: {exception.Message}");
            }
        }

        /// <summary>
        /// Altera o Status do Pedido
        /// </summary>
        /// <param name="updateSalesOderStatus"></param>
        /// <returns></returns>
        [HttpPatch]
        public ActionResult<SalesOrder> Patch([FromBody] UpdateSalesOderStatus updateSalesOderStatus)
        {
            try
            {
                _logger.LogInformation("Received post request");
                OrderStatus orderStatusEnum = (OrderStatus)Enum.Parse(typeof(OrderStatus), updateSalesOderStatus.OrderSatus);

                var result = _salesOrderServices.UpdateStatus(updateSalesOderStatus.SalesOderId, orderStatusEnum);

                if (result)
                    return Ok("Sucesso: Status atualizado.");

                return BadRequest("Erro: Status não atualizado.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return BadRequest($"Erro: {exception.Message}");
            }
        }
    }
}