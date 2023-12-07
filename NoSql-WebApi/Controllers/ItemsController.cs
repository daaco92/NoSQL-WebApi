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
        public async Task<IEnumerable<Items>> GetAllItems()
        {
            return await _context.ScanAsync<Items>(default).GetRemainingAsync();
            
        }

        // Decide what should be provided and returned, maybe an object of item?
        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItemById(int itemId)
        {
            var item = await _context.LoadAsync<Items>(itemId);
            if (item == null) return NotFound();
            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem(Items _item)
        {
            var item = await _context.LoadAsync<Items>(_item.Id);
            if (item != null) return BadRequest($"Item with Id {item.Id} already exists.");
            await _context.SaveAsync(item);
            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            var item = await _context.LoadAsync<Items>(itemId);
            if (item == null) return NotFound();
            await _context.DeleteAsync(item);
            return Ok("Object deleted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(Items _item)
        {
            var item = await _context.LoadAsync<Items>(_item.Id);
            if (item == null) return NotFound();
            await _context.SaveAsync(_item);
            return Ok(_item);
        }
    }
}
