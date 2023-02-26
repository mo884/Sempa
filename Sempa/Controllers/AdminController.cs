using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Sempa.Models;
using Sempa.ViewModel;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Sempa.Controllers
{
    public class AdminController : Controller
    {
        mtDbContext Db;
        public AdminController(mtDbContext Db)
        {
            this.Db = Db;
        }
        public IActionResult GetAll()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                ViewBag.Show = Db.users.ToList();
                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.Curr = CurrentID;
                return View();
            }
            return RedirectToAction("Login", "Users");

        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.crr = CurrentID;
                return View();

            }
            return RedirectToAction("Login", "Users");
        }
        [HttpPost]
        public IActionResult CreateUser(User user, IFormFile Image)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                var OldEmail = Db.users.SingleOrDefault(u => u.Email == user.Email);

                if (Image == null && OldEmail != null && user.Name == null && user.gender == null && user.Email == null && user.Adress == null && Image.FileName == null)
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
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult Delete(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                User use = Db.users.Where(p => p.ID == id).SingleOrDefault();
                Postscs Post = Db.posts.Where(p => p.ID == id).SingleOrDefault();
                Comments coment = Db.comments.Where(p => p.ID == id).SingleOrDefault();
                Flight flight = Db.flights.Where(p => p.ID == id).SingleOrDefault();
                Ticket ticket = Db.tickets.Where(p => p.ID == id).SingleOrDefault();
                if (use != null)
                {
                    Db.Remove(use);
                    Db.SaveChanges();
                    return RedirectToAction("GetAll");
                }
                else if (Post != null)
                {
                    Db.Remove(Post);
                    Db.SaveChanges();
                    return RedirectToAction("GetAllPosts");
                }
                else if (coment != null)
                {

                    Db.Remove(coment);
                    Db.SaveChanges();
                    return RedirectToAction("GetAllPosts");
                }
                else if (flight != null)
                {

                    Db.Remove(flight);
                    Db.SaveChanges();
                    return RedirectToAction("GetAllFlight");
                }
                else if (ticket != null)
                {

                    Db.Remove(ticket);
                    Db.SaveChanges();
                    return RedirectToAction("GetAllTicket");
                }
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult GetAllPosts()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                List<Postscs> Posts = Db.posts.Include(p => p.user).ToList();

                ViewBag.Curr = CurrentID;
                ViewBag.Post = Posts;
                return View();
            }
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Comments(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {

                Postscs cm = Db.posts.Where(c => c.ID == id).Include(i => i.comments).ThenInclude(n => n.user).SingleOrDefault();
                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.crr = CurrentID;
                ViewBag.com = cm;
                return View();
            }
            return RedirectToAction("Login", "Users");
        }
        [HttpGet]
        public IActionResult CreateTicket()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                List<Flight> flight = Db.flights.ToList();
                ViewBag.Flight = flight;

                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.crr = CurrentID;
                return View();
            }
            return RedirectToAction("Login", "Users");
        }
        [HttpPost]
        public IActionResult CreateTicket(Ticket ticket)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
               if(ticket == null)
                {
                    return RedirectToAction("CreateTicket", "Admin");
                }
                Db.tickets.Add(ticket);
                Db.SaveChanges();
                return RedirectToAction("GetAllTicket");
            }
            return RedirectToAction("Login", "Users");
        }
        [HttpGet]
        public IActionResult CreateFlight()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {

                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.crr = CurrentID;
                return View();
            }
       
          return RedirectToAction("Login", "Users");
        }
        [HttpPost]
        public IActionResult CreateFlight(Flight flight)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                if (flight == null)
                {
                    return RedirectToAction("CreateFlight", "Admin");
                }
                Db.flights.Add(flight);
                Db.SaveChanges();
                return RedirectToAction("GetAllFlight");
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult GetAllFlight()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                List<Flight> flights = Db.flights.ToList();
                ViewBag.Flight = flights;
                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.crr = CurrentID;
                return View();
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult GetAllTicket()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                List<Ticket> tickets = Db.tickets.Include(u => u.flight).ToList();
                ViewBag.Flight = tickets;
                User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                ViewBag.crr = CurrentID;
                return View();
            }
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Search(Search search)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
            {
                return RedirectToAction("Login", "Users");

            }
            else if (UserId == 5002)
            {
                
                if (search.Text == null)
                {
                    return RedirectToAction("GetAll");
                }
                List<User> use = Db.users.Where(u => u.Name.Contains(search.Text) || u.ID.ToString().Equals(search.Text.ToString()) ).ToList();
                if(use == null)
                {
                    return RedirectToAction("GetAll");
                }else
                {
                    User CurrentID = Db.users.Where(u => u.ID == UserId).Include(u => u.posts).SingleOrDefault();
                    ViewBag.crr = CurrentID;
                    ViewBag.SearchUSe = use;
                    return View();
                }
                
            }
            return RedirectToAction("Login", "Users");
        }

    }
  }
