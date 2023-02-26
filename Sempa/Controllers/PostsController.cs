using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Sempa.Models;
using Sempa.ViewModel;
using System.Collections;
using System.Net;

namespace Sempa.Controllers
{
    public class PostsController : Controller
    {
        mtDbContext Db;
        public PostsController(mtDbContext Db)
        {
            this.Db = Db;
        }
        public IActionResult Communication(Postscs post)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            List<Postscs> Posts = Db.posts.Include(p => p.user).ToList();
           
            ViewBag.Curr = CurrentID;
            ViewBag.Post = Posts;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
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
        public IActionResult Create(Postscs post, IFormFile Image)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            if(post.Title == null)
            {
                post.Title = " ";
            }
            string Path = $"wwwroot/Images/{Image.FileName}";
            FileStream s = new FileStream(Path, FileMode.Create);
            Image.CopyTo(s);
            post.Image = $"/Images/{Image.FileName}";
            post.UserId = HttpContext.Session.GetInt32("UserId");

            Db.posts.Add(post);
            Db.SaveChanges();
            return RedirectToAction("Communication");
        }

        [HttpGet]
        public IActionResult Edite(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            ViewBag.Curr = CurrentID;
            Postscs post = Db.posts.Where(p => p.ID == id).SingleOrDefault();
            ViewBag.Pos = post;
            return View();

        }

        [HttpPost]
        public IActionResult Edite(Postscs p , IFormFile Image)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            var post = Db.posts.FirstOrDefault(e => e.ID == p.ID);
            string Pathes;
            if (Image != null)
            {
                string Path = $"wwwroot/Images/{Image.FileName}";
                FileStream s = new FileStream(Path, FileMode.Create);
                Image.CopyTo(s);
                p.Image = $"/Images/{Image.FileName}";
                Pathes = p.Image;
            }
            else
            {

                Pathes = post.Image;
            }
            if (p.Body != null)
            {
                post.Body = p.Body;
            }
            post.Image = Pathes;
            Db.SaveChanges();

            return RedirectToAction("Profile","Users");

        }
        

        public IActionResult Delete(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            Postscs post = Db.posts.Where(p => p.ID == id).SingleOrDefault();
            if(post != null)
            {
                Db.Remove(post);
                Db.SaveChanges();
            }
           
            return RedirectToAction("Profile","Users");
        }
    }
}
