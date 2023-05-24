using APP.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BugsController(ApplicationDbContext context)
        {
            this.context = context;
        }



        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            var product = context.Products.Find(50);
            if(product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            var product = context.Products.Find(50);
            product.Name = "";
            return Ok();
        }


        [HttpGet("bad-request/id")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }



        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }


    }
}
