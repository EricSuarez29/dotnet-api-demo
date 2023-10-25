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
        return _context.Categories.Where(e => e.Status.Equals(1)).ToList();
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Update(Category category)
    {
        int Id = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Categories.Find(Id);
        category.Id = entity.Id;
        category.Name ??= entity.Name;
        category.Description ??= entity.Description;
        category.Status = entity.Status;
        _context.Categories.Entry(entity).CurrentValues.SetValues(category);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        category.Status = 1;
        _context.Categories.Add(category);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("soft/{id}")]
    public ActionResult Soft()
    {
        int Id = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Categories.Find(Id);
        entity.Status = 0;
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
