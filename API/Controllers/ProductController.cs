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
        return _context.Products.Where(e => e.Status.Equals(1)).ToList();
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        product.Status = 1;
        _context.Products.Add(product);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Update(Product product)
    {
        int Id = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Products.Find(Id);
        product.Id = entity.Id;
        product.Name ??= entity.Name;
        product.Description ??= entity.Description;
        product.Price = entity.Price;
        product.Quantity = entity.Quantity;
        product.Status = entity.Status;
        _context.Products.Entry(entity).CurrentValues.SetValues(product);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("soft/{id}")]
    public ActionResult Soft()
    {
        int Id = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Products.Find(Id);
        entity.Status = 0;
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
