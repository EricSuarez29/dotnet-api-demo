using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    public UserController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        return _context.Users.Where(e => e.Status.Equals(1)).ToList();
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Update(User user)
    {
        int userId = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Users.Find(userId);
        user.Id = entity.Id;
        user.Name ??= entity.Name;
        user.Email ??= entity.Email;
        user.Phone ??= entity.Phone;
        user.Password ??= entity.Password;
        user.Status = entity.Status;
        _context.Users.Entry(entity).CurrentValues.SetValues(user);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPost]
    public ActionResult Post(User user)
    {
        user.Status = 1;
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("soft/{id}")]
    public ActionResult Soft()
    {
        int userId = int.Parse(RouteData.Values["id"].ToString());
        var entity = _context.Users.Find(userId);
        entity.Status = 0;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete()
    {
        int userId = int.Parse(RouteData.Values["id"].ToString());
        User user = new User() { Id = userId };
        _context.Users.Remove(user);
        _context.SaveChanges();
        return Ok();
    }
}
