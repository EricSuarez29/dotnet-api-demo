using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly DataContext _context;
    public OrderController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get()
    {
        return _context.Orders.ToList();
    }

    [HttpPost]
    public ActionResult Post(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete()
    {
        int orderId = int.Parse(RouteData.Values["id"].ToString());
        Order order = new Order() { Id = orderId };
        _context.Orders.Remove(order);
        _context.SaveChanges();
        return Ok();
    }
}
