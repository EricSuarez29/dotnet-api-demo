using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly DataContext _context;
    public ProductController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return _context.Products.ToList();
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete()
    {
        int productId = int.Parse(RouteData.Values["id"].ToString());
        Product product = new Product() { Id = productId };
        _context.Products.Remove(product);
        _context.SaveChanges();
        return Ok();
    }
}
