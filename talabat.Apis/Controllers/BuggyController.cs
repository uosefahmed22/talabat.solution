using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Apis.Errors;
using talabat.Repository;

namespace talabat.Apis.Controllers
{
  
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet("Notfound")] //Get : /api/Buggy/notFound
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(100);
            if (product is null) return NotFound(new ApiResponse(404));
                    return Ok(product);
        }


        [HttpGet("serverError")] //Get : /api/Buggy/serverError
        public ActionResult GetserverError()
        {
            var product = _dbContext.Products.Find(100);
            var ProductToReturn = product.ToString(); // will trow exeption [ Null Reference Exeption]
            return Ok(ProductToReturn);
        }

        [HttpGet ("badRequest")] //Get : /api/Buggy/badRequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400 , "ziad"));
        }



        [HttpGet("badRequest/{id}")] //Get : /api/Buggy/badRequest/1
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }




    }
}
