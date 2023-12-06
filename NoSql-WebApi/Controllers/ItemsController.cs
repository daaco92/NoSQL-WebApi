using NoSql_WebApi.Models.Domain;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace NoSql_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IDynamoDBContext _context;
        
        public ItemsController(IDynamoDBContext context)
        {
                _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemModel>> GetAll()
        {
            return await _context.ScanAsync<ItemModel>(default).GetRemainingAsync();
            
        }
    }
}
