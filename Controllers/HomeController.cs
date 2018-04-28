using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using dojoactivity.Models;

namespace dojoactivity.Controllers
{
    public class HomeController : Controller
    {
        
        private DojoContext _context;

        public HomeController(DojoContext context)
        {
            _context = context;
        }
        
        //Login-Registration Page
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        //Register route
        [HttpPost]
        [Route("Register")]
         public IActionResult register(RegisterViewModel registerVM)
        { 
            if(ModelState.IsValid)
            {
                //Check if user already exists
                List<User> ReturnValues = _context.Users.Where(u => u.Email.Equals(registerVM.Email)).ToList();
                foreach (var item in ReturnValues)
                {
                    if(item.Email == registerVM.Email)
                        {
                            TempData["error"] ="Email already registered. Please log-in.";
                            return RedirectToAction("Index");  
                        }
                } 
                User user = new User
                {
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Email = registerVM.Email,
                    Password = registerVM.Password,
                };

                //Hashed Password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);

                //Save to DB
                _context.Add(user);
                _context.SaveChanges();

                //set userid into session
                HttpContext.Session.SetInt32("user_id", user.UserId);
                return RedirectToAction("Home");
            }
            return View("Index");
        }
        
        [HttpPost]
        [Route("Login")]
        public IActionResult login(string email, string password)
        {   
             if(ModelState.IsValid)
             {
                    List<User> ReturnedValues = _context.Users.Where(s => s.Email.Equals(email)).ToList();
                    
                    foreach (var users in ReturnedValues)
                    {                    
                            var Hasher = new PasswordHasher<User>();
                            // Pass the user object, the hashed password, and the PasswordToCheck
                            if(0 != Hasher.VerifyHashedPassword(users, users.Password, password))
                            {
                                //Handle success and Set ID and Name to session
                                HttpContext.Session.SetInt32("user_id", users.UserId);
                                return RedirectToAction("Home");
                            }
                            TempData["logerror"] ="Invalid email or password.";
                            return View("Index");
                    }
            };
            TempData["logerror"] ="Invalid email or password.";
            return View("Index");
        }
        
        [HttpGet]
        [Route("Home")]
        public IActionResult Home() 
        {
            int? userid = HttpContext.Session.GetInt32("user_id");
            ViewBag.Id = userid;

            //Check session if null
            if(userid != null)
            {
                 //get joining query
                 var ReturnedValues = _context.Activities.Include(r => r.User).Include(u => u.participants).ThenInclude( m => m.User).ToList();
                 ViewBag.display = ReturnedValues;

                 //get username
                 ViewBag.Username = _context.Users.SingleOrDefault(u => u.UserId == userid);
            }
            else {
                 return RedirectToAction("Index");
            }
            return View("Dashboard");
        }

        [HttpGet]
        [Route("New")]
        public IActionResult New() 
        {
             //Check session if null
            int? userid = HttpContext.Session.GetInt32("user_id");

            if(userid != null)
            {
                return View("AddActivity");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("activities")]
        public IActionResult activities(AddActivityViewModel newactivity)
        {
            int? userid = HttpContext.Session.GetInt32("user_id");
            int id = (int)userid;
            
            if(userid == null)
            {
                RedirectToAction("Index");
            }
            
            if(ModelState.IsValid)
            { 
                if (newactivity.Date < DateTime.Today) 
                {
                    TempData["error"] = "Date should be future-date";
                    return View("AddActivity");
                }
                
                Planner newactive = new Planner {
                    UserId = id,
                    Title = newactivity.Title,
                    Date = newactivity.Date,
                    Time = newactivity.Time,
                    Duration = newactivity.Duration,
                    Hours = newactivity.Hours,
                    Description = newactivity.Description
                };
                    _context.Add(newactive);
                    _context.SaveChanges();

                    return RedirectToAction("activity","Home", new { activityID = newactive.PlannerId});
            }
            return View("AddActivity");
        }

        [HttpGet]
        [Route("activity/{activityID}")]
        public IActionResult activity(int activityID)
        {
            List<Planner> ReturnedValues = _context.Activities.Where(w => w.PlannerId == activityID).Include(r => r.User).Include(u => u.participants).ThenInclude( m => m.User).ToList();
            ViewBag.Result = ReturnedValues;
            return View("PlanDetails");
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult delete(int planid)
        {
            Planner Delactivity = _context.Activities.FirstOrDefault(a => a.PlannerId == planid);
            _context.Activities.Remove(Delactivity);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("leave")]
        public IActionResult leave(int planid)
        {
            Participant Delactivity = _context.Participants.FirstOrDefault(a => a.PlannerId == planid);
            _context.Participants.Remove(Delactivity);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("join")]
        public IActionResult join(int planid)
        {
            Participant newparticipant = new Participant
            {
                PlannerId = planid,
                UserId = (int) HttpContext.Session.GetInt32("user_id")
            };
            _context.Participants.Add(newparticipant);
            _context.SaveChanges();
            return RedirectToAction("Home"); 
        }
        
        [HttpPost]
        [Route("activity/{name}")]
        public IActionResult add(int planid)
        {
            int? userid = HttpContext.Session.GetInt32("user_id");
            ViewBag.Id = userid;
            
            Participant newparticipant = new Participant
            {
                PlannerId = planid,
                UserId = ViewBag.Id
            };
            _context.Participants.Add(newparticipant);
            _context.SaveChanges();
            return RedirectToAction("Home"); 
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return Redirect("/");
         }
    }
}
