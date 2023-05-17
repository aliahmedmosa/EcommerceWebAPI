using APP.Api.Dtos;
using APP.Core.Entities;
using APP.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork uOW;

        public CategoriesController(IUnitOfWork uOW)
        {
            this.uOW = uOW;
        }



        [HttpGet("Get-All-Categories")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var allCategories = await uOW.CategoryRepository.GetAllAsync();
                if (allCategories is not null)
                {

                    /*
                      * add custom shape for return data ...
                        ----------------------manual Mapping
                     */
                    var res = allCategories.Select(x => new CategoryDto
                    {
                        Name = x.Name,
                        Description = x.Description
                    }).ToList();
                    return Ok(res);
                }

                return BadRequest("Not Found");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpGet("Get-Category-By-ID/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                /*
                 * ----------------manual Mapping
                 * ---- To get custom data by DTO
                 
                        var category = await uOW.CategoryRepository.GetAsync(id);
                        if (category == null)
                            return BadRequest($"Not Found This Id[{id}]");

                        var result = new CategoryDto
                        {
                            Name=category.Name,
                            Description=category.Description
                        };
                        return Ok(result);
                 *
                 */


                // ---- To get pure data as the entity ......

                var category = await uOW.CategoryRepository.GetAsync(id);
                if (category == null)
                    return BadRequest($"Not Found This Id[{id}]");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [HttpPost("Add-New-Category")]
        public async Task<ActionResult> post(CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = new Category
                    {
                        Name = categoryDto.Name,
                        Description = categoryDto.Description
                    };
                    await uOW.CategoryRepository.AddAsync(newCategory);
                    return Ok(categoryDto);
                }
                return BadRequest(categoryDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }




        [HttpPut("Update-Category-by-id")]
        public async Task<ActionResult> Put(int id, CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var exitingCategory = await uOW.CategoryRepository.GetAsync(id);
                    if (exitingCategory is not null)
                    {
                        exitingCategory.Name = categoryDto.Name;
                        exitingCategory.Description = categoryDto.Description;
                        await uOW.CategoryRepository.UpdateAsync(id, exitingCategory);
                        return Ok(categoryDto);
                    }
                    return BadRequest($"Category Not found , ID : [{id}] Incorrect");
                }
                return BadRequest("Data not valied");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("Delete-By-Id")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var exitingCategory = await uOW.CategoryRepository.GetAsync(id);
                if (exitingCategory is not null)
                {
                    await uOW.CategoryRepository.DeleteAsync(id);
                    return Ok($"this category [{exitingCategory.Name}] is successfully deleted ...");
                }
                return BadRequest($"Category Not found , ID : [{id}] Incorrect");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




    }
}
