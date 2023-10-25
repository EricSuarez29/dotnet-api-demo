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
        return _context.Orders.Where(e => e.Status.Equals(1)).ToList();
    }

    [HttpPost]
    public ActionResult Post(Order order)
    {
        order.Status = 1;
        _context.Orders.Add(order);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Update(Order order)
    {
        int Id = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Orders.Find(Id);
        order.Id = entity.Id;
        order.ProductId ??= entity.ProductId;
        order.UserId ??= entity.UserId;
        order.Quantity ??= entity.Quantity;
        order.Total ??= entity.Total;
        order.Status = entity.Status;
        _context.Orders.Entry(entity).CurrentValues.SetValues(order);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("soft/{id}")]
    public ActionResult Soft()
    {
        int Id = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Orders.Find(Id);
        entity.Status = 0;
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
