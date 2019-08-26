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
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ILogger _logger;

        public CreditCardController(ICreditCardService creditCardService, ILogger<CreditCardController> logger)
        {
            _creditCardService = creditCardService;
            _logger = logger;
        }
        [HttpPost("{salesOrderId}")]
        public ActionResult<string> Post([FromRoute]long salesOrderId, [FromBody] CreditCardViewModel creditCard)
        {
            try
            {
                _logger.LogInformation("Received POST request");

                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(creditCard);
                    _creditCardService.Insert(salesOrderId, json);
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
