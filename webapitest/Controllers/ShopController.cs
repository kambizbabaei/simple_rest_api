using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapitest.DataContext;

namespace webapitest.Controllers
{
    [ApiController]
    [Route("Shop")]
    public class ShopController :ControllerBase
    {
        private readonly Context _context;

        public ShopController(Context context)
        {
            _context = context;
        }

        [HttpGet("TestController")]
        public object TestShopController()
        {
            return new OkObjectResult(new { Result = "This is Shop Controller Working !!" });
        }
        
        [HttpGet("getItemFromShop")]
        public async Task<object> GetItemFromShop(int itemId)
        {
            var x = (await _context.Items.FirstOrDefaultAsync(x => x.Id == itemId));
            if(x != null)
                return new OkObjectResult(new {Result = x});
            return new NotFoundResult(); 
        }
        
        [HttpGet("buyItemFromShop")]
        public async Task<object> BuyItemFromShop(int itemId)
        {
            var x = (await _context.Items.FirstOrDefaultAsync(x => x.Id == itemId));
            if (x != null)
            {
                if (x.Count > 0)
                {
                    x.Count -= 1;
                    _context.Attach(x);
                    if(await _context.SaveChangesAsync() > 0)
                        return new OkObjectResult(new {Result = x});
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
                }
                return new StatusCodeResult(StatusCodes.Status406NotAcceptable);  
            }
            return new NotFoundResult(); 
        }
    }
}