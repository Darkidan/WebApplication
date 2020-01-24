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
    public class TopicsController : Controller
    {
        private CommunityDbContext db = new CommunityDbContext();

        // GET: Topics
        public ActionResult Index()
        {
            var topics = db.Topics.Include(t => t.Author).Include(t => t.Forum);
            return View(topics.ToList());
        }

        public ActionResult ViewForum(int? id, string searchBy, string searchValue)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(id);

            if (forum == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForumID = id;
            ViewBag.CountForum = forum.Topics.Count;

            var topics = db.Topics.Include(t => t.Author).Include(t => t.Forum).Where(x=> x.ForumID == forum.ForumID);
            
            if (searchBy == "TopicTitle")
                return View(topics.Where(x => x.TopicTitle.Contains(searchValue)));
            else
                return View(topics.ToList());
        }

        // GET: Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public ActionResult Create()
        { 
            ViewBag.UserID = Session["UserName"];
            ViewBag.ForumID = new SelectList(db.Forums, "ForumID", "ForumName");
            if ((string)Session["UserGroup"] == "User" || (string)Session["UserGroup"] == "Admin")
                return View();
            else
                return RedirectToAction("Login", "Users", null);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,TopicTitle,Content,NumOfComments,ForumID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                Forum forum = db.Forums.Find(topic.TopicID);
                int id = topic.ForumID;
                topic.UserID = (int)Session["UserID"];
                db.Topics.Add(topic);
                db.SaveChanges();
                ViewBag.ForumName = topic.ForumID;
                return RedirectToAction("ViewForum", "Topics", new { id });
            }
            ViewBag.UserID = Session["UserName"];
            ViewBag.ForumID = new SelectList(db.Forums, "ForumID", "ForumName", topic.ForumID);
            return View(topic);
        }

        // GET: Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", topic.UserID);
            ViewBag.ForumID = new SelectList(db.Forums, "ForumID", "ForumName", topic.ForumID);
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,TopicTitle,UserID,Content,NumOfComments,ForumID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", topic.UserID);
            ViewBag.ForumID = new SelectList(db.Forums, "ForumID", "ForumName", topic.ForumID);
            return View(topic);
        }

        // GET: Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
