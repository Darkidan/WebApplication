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
    public class ForumController : Controller
    {
        private CommunityDbContext db = new CommunityDbContext();

        // GET: Forum
        public ActionResult Index()
        {
            var forums = db.Forums.Include(f => f.category);
            return View(forums.ToList());
        }

        public ActionResult ViewForum(int? id)
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
            return View(forum.Topics.ToList());
        }

        // GET: Forum/Details/5
        public ActionResult Details(int? id)
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
            return View(forum);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CatagoryName");
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumID,ForumName,ForumDescription,NumOfTopics,CategoryID,Icon,background")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Forums.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CatagoryName", forum.CategoryID);
            return View(forum);
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CatagoryName", forum.CategoryID);
            return View(forum);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumID,ForumName,ForumDescription,NumOfTopics,CategoryID, Icon,background")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CatagoryName", forum.CategoryID);
            return View(forum);
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int? id)
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
            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forums.Find(id);
            db.Forums.Remove(forum);
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
