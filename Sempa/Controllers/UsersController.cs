using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Plugins;
using Sempa.Models;
using Sempa.ViewModel;

namespace Sempa.Controllers
{
    public class UsersController : Controller
    {
        mtDbContext Db;
        public UsersController(mtDbContext Db)
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
            ViewBag.Show = Db.users.ToList();
            return View();
        }
        public IActionResult Profile()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login");

            }



            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            return View(CurrentID);
        }
        [HttpGet]
        public IActionResult Edite(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login");

            }
            User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
            return View(CurrentID);
        }
        [HttpPost]
        public IActionResult Edite(User user, IFormFile Image)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login");

            }
            var Users = Db.users.FirstOrDefault(e => e.ID == user.ID);
            string Pathes;
            if (Image != null)
            {
                string Path = $"wwwroot/Images/{Image.FileName}";
                FileStream s = new FileStream(Path, FileMode.Create);
                Image.CopyTo(s);
                user.Image = $"/Images/{Image.FileName}";
                Pathes = user.Image;
            }else
            {
                Pathes = Users.Image;
            }

           
           
            Users.Name = user.Name;
            Users.Adress = user.Adress;

            Users.Password = user.Password;
            Users.School = user.School;
            Users.Image = Pathes;
            Db.SaveChanges();
            return RedirectToAction("Profile");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user, IFormFile Image)
        {
           var OldEmail = Db.users.SingleOrDefault(u => u.Email == user.Email);
         
            if( Image == null && OldEmail != null && user.Name == null && user.gender == null && user.Email == null && user.Adress == null && Image.FileName == null)
            {
                return RedirectToAction("Create");
            }
           
            //if(user.Email.ToString().Equals(OldEmail.Email.ToString()) )
            //{
            //    return RedirectToAction("Create");
            //}else
           
                string Path = $"wwwroot/Images/{Image.FileName}";
                FileStream s = new FileStream(Path, FileMode.Create);
                Image.CopyTo(s);
                user.Image = $"/Images/{Image.FileName}";

                Db.users.Add(user);
                Db.SaveChanges();
                return View();
            
            
        }
        public IActionResult Like(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login");

            }
            Postscs post = Db.posts.Where(p => p.ID == id).SingleOrDefault();
            if (post != null)
            {
                post.PostLike++;
            }
            Db.SaveChanges();
            return RedirectToAction("Communication" , "Posts");
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginVM log = new LoginVM();
            return View(log);
        }
        [HttpPost]
        public IActionResult Login(LoginVM login)
        {
            User use = Db.users.SingleOrDefault(u => u.Email == login.Email && u.Password == login.Passward);
            if (use == null)
            {
                LoginVM log = new LoginVM();
                log.Message = "Wrong Email or Passward !";
                return View("Login", log);
            }
            if(use.Email == "Admin@gmail.com" && use.Password == "Admin2000")

            {
                HttpContext.Session.SetInt32("UserId", use.ID);
                return RedirectToAction("GetAll" ,"Admin");

            }
            else
            {
                HttpContext.Session.SetInt32("UserId", use.ID);
                return RedirectToAction("Profile");
            }
           


        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
