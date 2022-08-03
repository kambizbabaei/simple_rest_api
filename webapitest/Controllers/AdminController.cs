using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapitest.DataContext;
using webapitest.Domains;

namespace webapitest.Controllers
{
    [ApiController]
    [Route("Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController :ControllerBase
    {
        private Context _context;

        public AdminController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public object TestAdminController()
        {
            return new OkObjectResult(new { Result = "This is Admin Controller Working !!" });
        }
        
        [HttpPost("addItemToShop")]
        public async Task<object> AddItemToShop([FromBody] Item item)
        {
            await _context.Items.AddAsync(item);
           
            if((await _context.SaveChangesAsync()) > 0)
                return new OkObjectResult(new {Result = "item saved!"});
            return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
        } 
        
        [HttpDelete("removeItemFromShop")]
        public async Task<object> RemoveItemFromShop(int id)
        {
            var x = _context.Items.FirstOrDefault(x => x.Id == id);
            if (x is not null)
            {
                _context.Items.Remove(x);
            }
            else
            {
                return new  NotFoundObjectResult(new {Result = "item not found!"});
            }
            
            if((await _context.SaveChangesAsync()) > 0)
                return new OkObjectResult(new {Result = x});
            
            return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
        }
        
    }
}