using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sempa.Models;
using System.Runtime.Intrinsics.X86;

namespace Sempa.Controllers
{
    public class CommentsController : Controller
    {
        mtDbContext Db;
        public CommentsController(mtDbContext Db)
        {
            this.Db = Db;
        }
        public IActionResult Comments(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }

            Postscs cm = Db.posts.Where(c => c.ID == id).Include(i => i.comments).ThenInclude(n => n.user).SingleOrDefault();
            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            ViewBag.crr = CurrentID;
            ViewBag.com = cm;
            return View();


        }
        public IActionResult Comm(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            Postscs cm = Db.posts.Where(c => c.ID == id).Include(i => i.comments).ThenInclude(n => n.user).SingleOrDefault();
            HttpContext.Session.SetInt32("PostId", id);
            return RedirectToAction("Create", "Comments");
        }

        public IActionResult Create(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
          
            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            ViewBag.Curr = CurrentID;
           
          
            return View();

        }
        [HttpPost]
        public IActionResult Create(Comments commentss, IFormFile Stickear)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            string Path;
            if (commentss.Stickear == null)
            {
                Path = "";
            }
            else
            {
                Path = $"wwwroot/Images/{Stickear.FileName}";
                FileStream s = new FileStream(Path, FileMode.Create);
                Stickear.CopyTo(s);
            }
           
            commentss.UserID = (int)UserId;
            commentss.PostID = (int)HttpContext.Session.GetInt32("PostId");
            commentss.Stickear = Path;
            Db.comments.Add(commentss);
            Db.SaveChanges();
            return RedirectToAction("Communication" , "Posts");
        }
    }
}

