using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.DbContexts;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private CommunityDbContext db = new CommunityDbContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,Username,Password,Age,Gender,Email,Avatar,NumOfTopics,NumOfComments,UserGroup")] User user)
        {
            if (ModelState.IsValid)
            {
                user.NumOfComments = 0;
                user.NumOfTopics = 0;
                user.UserGroup = "User";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password, UserGroup")] User user)
        {
            User loginUser = null;
            foreach(User s in db.Users)
            {
                if (s.Password == user.Password && s.Username == user.Username)
                {
                    loginUser = s;
                    break;
                }
            }

            if (loginUser != null)
            {
                Session["UserName"] = loginUser.Username;
                Session["UserGroup"] = loginUser.UserGroup;
                Session["Password"] = loginUser.Password;
                Session["Avatar"] = loginUser.Avatar;
                Session["UserID"] = loginUser.UserID;

              return RedirectToAction("Index", "Home", null);
            }
   
            else
            {
                ViewBag.Error = "Username or Password is Invalid!";
                return View();
            }

        }
        

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["UserGroup"] = null;
            Session["Password"] = null;
            return RedirectToAction("Index", "Home", null);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Password,Age,Gender,Email,Avatar,NumOfTopics,NumOfComments,UserGroup")] User user)
        {
            if (ModelState.IsValid)
            {
                user.NumOfComments = 0;
                user.NumOfTopics = 0;
                user.UserGroup = "User";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home",null);
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Password,Age,Gender,Email,Avatar,NumOfTopics,NumOfComments,UserGroup")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
