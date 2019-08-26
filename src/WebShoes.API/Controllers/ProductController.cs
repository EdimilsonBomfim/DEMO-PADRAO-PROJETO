using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShoes.API.ViewModel;
using WebShoes.Domain.Entities;
using WebShoes.Service.Interfaces;

namespace WebShoes.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productservice"></param>
        /// <param name="logger"></param>
        public ProductController(IProductService productservice, ILogger<ProductController> logger)
        {
            _productService = productservice;
            _logger = logger;
        }
        /// <summary>
        /// Retorna a relação geral de produtos cadastradaos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                _logger.LogInformation("Received GET request...");
                return Ok(_productService.GetAll());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }
        /// <summary>
        /// Retorna a apenas um produdo cadastrado
        /// </summary>
        /// <returns></returns>
        [HttpGet("produto/{id:long}")]
        public ActionResult<Product> GetById([FromRoute] long id)
        {
            try
            {
                _logger.LogInformation("Received GET BY ID request...");
                return Ok(_productService.GetById(id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Inclui um novo produto
        /// </summary>
        /// <param name="productviewmodel">Parametros para criação de produtos</param>
        /// <returns>Sucesso ou Erro.</returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody] ProductViewModel productviewmodel)
        {
            try
            {
                _logger.LogInformation("Received POST request");

                if (ModelState.IsValid)
                {
                    _productService.Add(productviewmodel.Code, 
                                        productviewmodel.Description, 
                                        productviewmodel.Quantity, 
                                        productviewmodel.UnitPrice);
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
        /// <summary>
        /// Retorna o preço unitário do produto
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <returns></returns>
        [HttpGet("price/{id:long}")]
        public ActionResult<Product> GetPrice([FromRoute] long id)
        {
            try
            {
                _logger.LogInformation("Received GET PRICE request...");
                return Ok(_productService.GetUnitPrice(id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }
        /// <summary>
        /// Retorna a quantidade disponível em estoque
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <returns>Retorna a quantidade de ítens em estoque</returns>
        [HttpGet("stock/{id:long}")]
        public ActionResult<Product> GetStock([FromRoute] long id)
        {
            try
            {
                _logger.LogInformation("Received GET STOCK request...");
                return Ok(_productService.GetStock(id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Retorna a descrição do produto
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <returns></returns>
        [HttpGet("description/{id:long}")]
        public ActionResult<Product> GetDescripton([FromRoute] long id)
        {
            try
            {
                _logger.LogInformation("Received GET DESCRIPTION request...");
                return Ok(_productService.GetDescription(id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Atualiza a quantidade de ítens em estoque  - SUBTRAIR
        /// </summary>
        /// <param name="id">ID do Produto</param>
        /// <param name="quantity">Quantidade para subtrair do estoque</param>
        [HttpPatch("decrease")]
        public bool PatchDecrease([FromBody] long id, int quantity)
        {
            try
            {
                _logger.LogInformation("Received POST DECREASE request");
                _productService.DecreaseQuantity(id, quantity);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Atualiza a quantidade de ítens em estoque - SOMAR
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="quantity">Quantidade para somar no estoque</param>
        [HttpPatch("increase")]
        public bool PatchIncrease([FromBody] long id, int quantity)
        {
            try
            {
                _logger.LogInformation("Received POST INCREASE request");
                _productService.IncreaseQuantity(id, quantity);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Alteração do preço unitário do produto
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="unitprice">Novo Preço Unitário</param>
        [HttpPatch("changeprice")]
        public bool PatchChangeUnitPrice([FromBody] long id, int unitprice)
        {
            try
            {
                _logger.LogInformation("Received POST CHANGE UNIT PRICE request");
                _productService.ChangeUnitPrice(id, unitprice);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return false;
            }
        }
    }
}