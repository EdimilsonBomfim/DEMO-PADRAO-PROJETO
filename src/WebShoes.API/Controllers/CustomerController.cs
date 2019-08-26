using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShoes.API.ViewModel;
using WebShoes.Domain;
using WebShoes.Repository.Interfaces;
using WebShoes.Services.Interfaces;

namespace WebShoes.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="logger"></param>
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                _logger.LogInformation("Received Post request ");

                return Ok(_customerService.Select());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{cpf}")]
        public ActionResult<Customer> Get(string cpf)
        {
            try
            {
                var customer =  _customerService.GetByCpf(cpf);

                if( customer != null)
                {
                    return customer;
                }
                return Ok("Cliente não localizado !");

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);

            }
        }

        //
        //POST product
        //
        [HttpPost]
        public ActionResult<string> Post([FromBody] CustomerViewModel customerviewmodel )
        {
            try
            {
                _logger.LogInformation("Received POST request");

                if (ModelState.IsValid)
                {
                    _customerService.Insert(customerviewmodel.Cpf,
                                            customerviewmodel.Email,
                                            customerviewmodel.Name);

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
