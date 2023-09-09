namespace HTTP.Server;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateItem(Item item) => Ok();
    
    [HttpPut("{id}")]
    public IActionResult UpdateItem(int id, Item item) => Ok();
    
    [HttpGet("{id}")]
    public IActionResult GetItem(int id) => Ok();

    [HttpDelete("{id}")]
    public IActionResult DeleteItem(int id) => Ok();
    
    [HttpPatch("{id}")]
    public IActionResult PatchItem(JsonPatchDocument<Item> patchDoc) => Ok();
}