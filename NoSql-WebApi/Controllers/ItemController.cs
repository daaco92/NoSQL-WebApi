using NoSql_WebApi.Models.Domain;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DocumentModel;
using System.Xml.Linq;

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


        //  TODO:
        //  [] Get items of a specific type (filter items based on type)

        // GET: api/item/{type}/{id}
        [HttpGet("{type}/{id}")]
        public async Task<ActionResult<Item>> GetItemById(string type, string id)
        {
            var item = await _context.LoadAsync<Item>(type, id);
            if (item == null) return NotFound();
            return Ok(item);
        }


        // GET: api/item
        [HttpGet]
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.ScanAsync<Item>(default).GetRemainingAsync();
            
        }

        // Decide what should be provided and returned, maybe an object of item?

        // GET: api/item/{string}
        [HttpGet("type")]
        public async Task<IEnumerable<Item>> GetByType(string type)
        {
            //var opConfig = new DynamoDBOperationConfig();
            //opConfig.QueryFilter = new List<ScanCondition>
            //{
            //    new ScanCondition("Type", ScanOperator.Equal, type)
            //};
            return await _context.QueryAsync<Item>(type).GetRemainingAsync();

        }

        // GET: api/item/{string}
        //[HttpGet("{type}")]
        //public async Task<ActionResult<IEnumerable<Item>>> GetItemByType(string type)
        //{
        //    var item = await _context.LoadAsync<Item>(type);
        //    if (item == null) return NotFound();
        //    return Ok(item);
        //}

        // POST: api/item
        [HttpPost]
        public async Task<IActionResult> CreateItem(Item _item)
        {
            var item = await _context.LoadAsync<Item>(_item.Type, _item.Id);
            if (item != null) return BadRequest();
            await _context.SaveAsync(_item);
            return Ok(_item);
        }

        // DELETE: api/item/{type}/{id}
        [HttpDelete("{type}/{id}")]
        public async Task<IActionResult> DeleteItem(string type, string id)
        {
            var item = await _context.LoadAsync<Item>(type, id);
            if (item == null) return NotFound();
            await _context.DeleteAsync(item);
            return Ok("Object deleted");
        }

        // PUT: api/item
        [HttpPut]
        public async Task<IActionResult> UpdateItem(Item _item)
        {
            var item = await _context.LoadAsync<Item>(_item.Type, _item.Id);
            if (item == null) return NotFound();
            await _context.SaveAsync(_item);
            return Ok(_item);
        }
    }
}
