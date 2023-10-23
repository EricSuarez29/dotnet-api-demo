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
        return _context.Users.ToList();
    }

    [HttpPost]
    public ActionResult Post(User user)
    {
        _context.Users.Add(user);
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
