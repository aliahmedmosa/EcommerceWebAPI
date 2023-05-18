using APP.Api.Dtos;
using APP.Core.Entities;
using APP.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork uOW;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork uOW, IMapper mapper)
        {
            this.uOW = uOW;
            this.mapper = mapper;
        }

        [HttpGet("Get-All-Products")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var allProducts = await uOW.ProductRepository.GetAllAsync(x=>x.Category);
                if (allProducts is not null)
                {
                    //Start implementaion
                    var response = mapper.Map<List<ProductDto>>(allProducts);
                    return Ok(response);

                    //End implementaion

                }
                return BadRequest("Not Found");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Get-Product-By-ID/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var Product = await uOW.ProductRepository.GetAsync(id,x=>x.Category);

                //Start implementaion

                if (Product is not null)
                {
                    var response = mapper.Map<ProductDto>(Product);
                    return Ok(response);

                }
                return BadRequest($"Not Found This Id[{id}]");

                //End implementaion

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






    }
}
