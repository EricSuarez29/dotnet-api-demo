using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly DataContext _context;
    public CategoryController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        return _context.Categories.ToList();
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete()
    {
        int categoryId = int.Parse(RouteData.Values["id"].ToString());
        Category category = new Category() { Id = categoryId };
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return Ok();
    }
}
