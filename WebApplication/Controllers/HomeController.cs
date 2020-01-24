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
    public class HomeController : Controller
    {
        private CommunityDbContext db = new CommunityDbContext();
        private MapsDbContext maps = new MapsDbContext();

        public ActionResult Index()
        {
            ViewBag.Icon = db.Forums;

            // implementation of group by
            var result = from cat in db.Categories
                         group cat by cat;

            
            int sum = 0; // The total sum of topics in all categories
            foreach (var p in result)
            {
                foreach (var j in p.Key.Forums)
                {
                    sum += j.Topics.Count();
                }
            }

            ViewBag.TotalTopics = sum;

            Maps mofo = null;
            foreach (Maps m in maps.Map)
            {
                mofo = m;
                break;
            }

            if (mofo != null)
            {
                ViewBag.Latitude = mofo.Latitude;
                ViewBag.Longtitude = mofo.Longitude;
            }
            else
            {
                ViewBag.Latitude = 51.122;
                ViewBag.Longtitude = 0;
            }

            ViewBag.TotalCategories = db.Categories.Count();
            ViewBag.TotalForums = db.Forums.Count();
            ViewBag.TotalComments = db.Comments.Count();
            ViewBag.TotalUsers = db.Users.Count();

                return View(db.Categories.ToList());
        }


        public ActionResult Search(string categoryVal, string forumVal, string topicVal)
        {
            if(categoryVal == "")
            {
                if(forumVal == "")
                {
                    if(topicVal == "")
                    {
                        var result = from s in db.Comments select s;
                        return View(result);
                    }
                    var result1 = from s in db.Comments where (s.CommentTopic.TopicTitle.Contains(topicVal)) select s;
                    return View(result1);
                }
                var result2 = from s in db.Comments where (s.CommentTopic.TopicTitle.Contains(topicVal) && s.CommentTopic.Forum.ForumName.Contains(forumVal)) select s;
                return View(result2);
            }

            else
            {
                if (forumVal == "")
                {
                    if (topicVal == "")
                    {
                        var result3 = from s in db.Comments where (s.CommentTopic.Forum.category.CatagoryName.Contains(categoryVal)) select s;
                        return View(result3);
                    }
                    var result4 = from s in db.Comments where (s.CommentTopic.TopicTitle.Contains(topicVal) && (s.CommentTopic.Forum.category.CatagoryName.Contains(categoryVal))) select s;
                    return View(result4);
                } 
            }
            var result5 = from s in db.Comments where (s.CommentTopic.TopicTitle.Contains(topicVal) && s.CommentTopic.Forum.ForumName.Contains(forumVal) && (s.CommentTopic.Forum.category.CatagoryName.Contains(categoryVal))) select s;
            return View(result5);
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CatagoryName,NumOfForums")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CatagoryName,NumOfForums")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
