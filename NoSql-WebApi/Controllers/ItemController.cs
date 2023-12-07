using NoSql_WebApi.Models.Domain;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace NoSql_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IDynamoDBContext _context;
        
        public ItemController(IDynamoDBContext context)
        {
                _context = context;
        }

        // GET: api/item
        [HttpGet]
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.ScanAsync<Item>(default).GetRemainingAsync();
            
        }

        // Decide what should be provided and returned, maybe an object of item?

        // GET: api/item/{int}/{string}
        [HttpGet("{id}/{type}")]
        public async Task<IActionResult> GetItemById(int id, string type)
        {
            var item = await _context.LoadAsync<Item>(id, type);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/item
        [HttpPost]
        public async Task<IActionResult> CreateItem(Item _item)
        {
            var item = await _context.LoadAsync<Item>(_item.Id, _item.Type);
            if (item != null) return BadRequest($"Item with Id {item.Id} already exists.");
            await _context.SaveAsync(_item);
            return Ok(_item);
        }

        // DELETE: api/item/{int}/{string}
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(int id, string type)
        {
            var item = await _context.LoadAsync<Item>(id, type);
            if (item == null) return NotFound();
            await _context.DeleteAsync(item);
            return Ok("Object deleted");
        }

        // PUT: api/item
        [HttpPut]
        public async Task<IActionResult> UpdateItem(Item _item)
        {
            var item = await _context.LoadAsync<Item>(_item.Id, _item.Type);
            if (item == null) return NotFound();
            await _context.SaveAsync(_item);
            return Ok(_item);
        }
    }
}
