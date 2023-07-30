using APP.Api.Errors;
using APP.Api.Helper;
using APP.Core.Dtos;
using APP.Core.Entities;
using APP.Core.Interfaces;
using APP.Core.Sharing;
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
        public async Task<ActionResult> Get([FromQuery]ProductParams productParams)
        {
            try
            {
                var allProducts = await uOW.ProductRepository.GetAllAsync(productParams);
                if (allProducts is not null)
                {
                    //Start implementaion
                    var result = mapper.Map<IReadOnlyList<ProductDto>>(allProducts.ProductDtos);
                    return Ok(new Pagination<ProductDto>(productParams.PageNumber, productParams.PageSize, allProducts.TotalItems, result)); //Generic Pagination class to format the response....
                      
                    //End implementaion

                }
                return NotFound(new BaseCommonResponse(404));
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
                return NotFound(new BaseCommonResponse(404));

                //End implementaion

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Add-New-Product")]
        public async Task<ActionResult> Post([FromForm]CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var response = mapper.Map<Product>(productDto);

                    var result =  await uOW.ProductRepository.AddAsync(productDto);
                    return result? Ok(productDto) : BadRequest(productDto);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("Update-Eciting-Product")]
        public async Task<ActionResult> Put([FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await uOW.ProductRepository.UpdateAsync(productDto);
                    return result ? Ok(productDto) : BadRequest(productDto);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("Delete-Product-By-Id")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var exitingProduct = await uOW.ProductRepository.GetAsync(id);
                if (exitingProduct is not null)
                {
                    await uOW.ProductRepository.DeleteAsync(id);
                    return Ok($"this Product [{exitingProduct.Name}] is successfully deleted ...");
                }
                return BadRequest($"Product Not found , ID : [{id}] Incorrect");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



    }
}
