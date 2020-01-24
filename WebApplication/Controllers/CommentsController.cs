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
    public class CommentsController : Controller
    {
        private CommunityDbContext db = new CommunityDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.CommentTopic).Include(c => c.CommentUser);
            return View(comments.ToList());
        }

        public ActionResult ViewTopic(int? id, string searchBy, string searchValue)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            ViewBag.TopicTitle1 = topic.TopicTitle;
            ViewBag.TopicID = id;
            if (topic == null)
            {
                return HttpNotFound();
            }

            var comments = db.Comments.Include(c => c.CommentTopic).Include(c => c.CommentUser).Where(x => x.TopicID == topic.TopicID);

            if (searchBy == "CommentContent")
                return View(comments.Where(x => x.CommentContent.Contains(searchValue)));
            else
                return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult CreateComment()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicTitle");
            ViewBag.UserID = Session["UserName"];
            if ((string)Session["UserGroup"] == "User" || (string)Session["UserGroup"] == "Admin")
                return View();
            else
                return RedirectToAction("Login", "Users", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentID,CommentContent,UserID,TopicID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                Topic topic = db.Topics.Find(comment.TopicID);              
                int id = comment.TopicID;

                User p = null;
                foreach(User f in db.Users)
                {
                    if (f.Username == (string)Session["UserName"] && f.Password == (string)Session["Password"])
                    {
                        p = f;
                        break;
                    }
                }
                comment.CommentUser = p;
                comment.CommentUser.Username = (string)Session["UserName"];
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ViewTopic", "Comments", new { id });
            }

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicTitle", comment.TopicID);
            ViewBag.UserID = Session["UserName"];
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicTitle");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,CommentContent,UserID,TopicID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicTitle", comment.TopicID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", comment.UserID);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicTitle", comment.TopicID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", comment.UserID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,CommentContent,UserID,TopicID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicTitle", comment.TopicID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", comment.UserID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
