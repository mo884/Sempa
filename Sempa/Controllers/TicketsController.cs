using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sempa.Models;

namespace Sempa.Controllers
{
    public class TicketsController : Controller
    {
        mtDbContext Db;
        public TicketsController(mtDbContext Db)
        {
            this.Db = Db;
        }
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login");

            }
            ViewBag.Ticket = Db.tickets.Include(u => u.flight).ToList();
            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            ViewBag.Curr = CurrentID;
            return View();
        }
        public IActionResult Record(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login");

            }

            Ticket ticket = Db.tickets.Where(p => p.ID == id).Include(p => p.Users).SingleOrDefault();
            User use = Db.users.Where(u => u.ID == UserId).Include(p => p.tickets).SingleOrDefault();
            ticket.Users.Add(use);
            use.tickets.Add(ticket);
            Db.SaveChanges();
            return Content("You will be contacted soon or you will find a call from us within 20 minutes");

        }

    }
}
