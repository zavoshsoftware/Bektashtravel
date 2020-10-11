using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;
using ViewModels;
using Helpers;

namespace Bisan.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        MenuHelper menu = new MenuHelper();
        // GET: Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogGroup).Where(b => b.IsDelete == false).OrderByDescending(b => b.SubmitDate);
            return View(blogs.ToList());
        }

        // GET: Blogs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadHeader)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }

                if (fileUploadHeader != null)
                {
                    string filename = Path.GetFileName(fileUploadHeader.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadHeader.SaveAs(physicalFilename);

                    blog.HeaderImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDelete = false;
                blog.SubmitDate = DateTime.Now;
                blog.LastModificationDate = DateTime.Now;
                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadHeader)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }

                if (fileUploadHeader != null)
                {
                    string filename = Path.GetFileName(fileUploadHeader.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUploadHeader.SaveAs(physicalFilename);

                    blog.HeaderImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDelete = false;
                blog.LastModificationDate = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
            blog.IsDelete = true;
            blog.DeleteDate = DateTime.Now;

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

        [AllowAnonymous]
        [Route("blog/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            BlogListViewModel blogListViewModel = new BlogListViewModel();

            BlogGroup blogGroup = db.BlogGroups.FirstOrDefault(current => current.UrlParam == urlParam);
            if (blogGroup == null)
                return RedirectPermanent("/");
            blogListViewModel.Menu = menu.ReturnMenuTours();
            blogListViewModel.Footer = menu.ReturnFooter();
            blogListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            blogListViewModel.BlogGroup = blogGroup;
            blogListViewModel.Blogs = db.Blogs.Where(current => current.IsDelete == false && current.BlogGroupId == blogGroup.Id).ToList();
            blogListViewModel.SidebarTourCategories = GetSideBarTourCategory();

            ViewBag.Title = blogGroup.PageTitle;
            ViewBag.Description = blogGroup.PageDescription;

            ViewBag.header = "/Images/header1.jpg";
            //ViewBag.header = "/Images/headers/guid.jpg";

            if (urlParam == "online-mag")
                ViewBag.header = "/images/headers/online-mag.jpg";

            else if (urlParam == "food-tour")
                ViewBag.header = "/images/headers/restourant.jpg";

            else if (urlParam == "travel-guide")
                ViewBag.header = "/images/headers/guid.jpg";

            else if (urlParam == "tourist-attractions")
                ViewBag.header = "/images/headers/attractions.jpg";

            return View(blogListViewModel);
        }

        [AllowAnonymous]
        [Route("blog")]
        public ActionResult PureList()
        {
            BlogListViewModel blogListViewModel = new BlogListViewModel();


            blogListViewModel.Menu = menu.ReturnMenuTours();
            blogListViewModel.Footer = menu.ReturnFooter();
            blogListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            blogListViewModel.Blogs = db.Blogs.Include(c => c.BlogGroup).Where(current => current.IsDelete == false).ToList();
            blogListViewModel.SidebarTourCategories = GetSideBarTourCategory();

            ViewBag.Title = "وبلاگ بکتاش سیر گشت";
            return View(blogListViewModel);
        }



        [AllowAnonymous]
        public List<TourCategory> GetSideBarTourCategory()
        {
            return db.TourCategories
                .Where(current => current.ParentId == null && current.IsDelete == false)
                .OrderByDescending(c => c.Priority).ToList();
        }

        [AllowAnonymous]
        [Route("blog/{categoryUrlParam}/{urlParam}")]
        public ActionResult Details(string categoryUrlParam, string urlParam)
        {
            Blog blog = db.Blogs.Include(c => c.BlogGroup).FirstOrDefault(current => current.UrlParam == urlParam);

            if (blog == null)
                return RedirectPermanent("/blog");

            if (blog.BlogGroup.UrlParam != categoryUrlParam)
                return RedirectPermanent("/blog/" + blog.BlogGroup.UrlParam + "/" + urlParam);

            blog.VisitNumber = blog.VisitNumber + 1;
            db.SaveChanges();

            BlogDetailViewModel blogDetail = new BlogDetailViewModel();
            blogDetail.Menu = menu.ReturnMenuTours();
            blogDetail.Footer = menu.ReturnFooter();
            blogDetail.MenuBlogGroups = menu.ReturnBlogGroups();
            blogDetail.Blog = blog;

            blogDetail.RecentBlogs = db.Blogs.Where(current => current.IsDelete == false && current.Id != blog.Id)
                .OrderByDescending(c => c.SubmitDate).Take(3).ToList();

            blogDetail.BlogGroups = db.BlogGroups.Where(current => current.IsDelete == false).ToList();
            blogDetail.Comments = ReturnComments(blog.Id);

            ViewBag.Title = blog.PageTitle;
            ViewBag.Description = blog.PageDescription;

            if (string.IsNullOrEmpty(blog.HeaderImageUrl))
                ViewBag.headerImg = "/Images/header1.jpg";
            else
                ViewBag.headerImg = blog.HeaderImageUrl;

            return View(blogDetail);
        }
        [AllowAnonymous]
        public List<Comment> ReturnComments(Guid id)
        {
            Guid typeId = new Guid("4b08f9fc-c536-419b-845c-d61af6c20ddf");
            List<Comment> comments = db.Comments.Where(current => current.TypeId == typeId && current.EntityId == id && current.IsDelete == false).ToList();
            return comments;
        }
        [AllowAnonymous]
        public ActionResult RequestPost(string fullname, string message, string mobile, string urlParam)
        {
            if (ModelState.IsValid)
            {
                Blog blog = db.Blogs.FirstOrDefault(c => c.UrlParam == urlParam);

                Guid typeId = new Guid("4b08f9fc-c536-419b-845c-d61af6c20ddf");
                Comment comment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    IsDelete = false,
                    SubmitDate = DateTime.Now,
                    Fullname = fullname,
                    Description = message,
                    Mobile = mobile,
                    EntityId = blog.Id,
                    TypeId = typeId,
                    IsShow = false
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else

                return Json("false", JsonRequestBehavior.AllowGet);
        }
    }
}
