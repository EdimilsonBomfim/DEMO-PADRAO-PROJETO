using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using Microsoft.Extensions.Logging;
using WebShoes.Services.Interfaces;
using Newtonsoft.Json;

namespace WebShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentSlipController : ControllerBase
    {
        private readonly IPaymentSlipService _paymentSlipService;
        private readonly ILogger _logger;

        public PaymentSlipController(IPaymentSlipService paymentSlipService, ILogger<PaymentSlipController> logger)
        {
            _paymentSlipService = paymentSlipService;
            _logger = logger;
        }
        [HttpPost("{salesOrderId}")]
        public ActionResult<string> Post([FromRoute]long salesOrderId,[FromBody] PaymentSlipViewModel paymentSlip)
        {
            try
            {
                _logger.LogInformation("Received POST request");

                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(paymentSlip);
                    _paymentSlipService.Insert(salesOrderId, json);
                    return Ok("Success");
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