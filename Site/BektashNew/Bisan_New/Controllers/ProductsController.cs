using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshop.Helpers;
using Helpers;
using Models;
using ViewModels;

namespace Bisan_New.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductGroup).Where(p => p.IsDelete == false).OrderByDescending(p => p.Code);
            return View(products.ToList());
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase fileUpload)
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

                    product.ImageUrl = newFilenameUrl;
                }
                #endregion
                CodeCreator cc = new CodeCreator();
                product.Code = cc.ReturnProductCode();
                product.IsDelete = false;
                product.SubmitDate = DateTime.Now;
                product.Id = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase fileUpload)
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

                    product.ImageUrl = newFilenameUrl;
                }
                #endregion
                product.IsDelete = false;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Title", product.ProductGroupId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            product.IsDelete = true;
            product.DeleteDate = DateTime.Now;

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


        MenuHelper menu = new MenuHelper();

        [AllowAnonymous]
        [Route("product/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            ProductGroupListViewModel oProductGroup = new ProductGroupListViewModel();
            ProductGroup productGroup = db.ProductGroups.FirstOrDefault(c => c.UrlParam == urlParam);

            if (productGroup != null)
            {

                oProductGroup.ProductGroup = productGroup;
                oProductGroup.Products = db.Products.Where(c => c.ProductGroupId == productGroup.Id && c.IsDelete == false).OrderBy(c => c.Order).ToList();
                oProductGroup.Menu = menu.ReturnMenuTours();
                oProductGroup.Footer = menu.ReturnFooter();
                oProductGroup.MenuBlogGroups = menu.ReturnBlogGroups();

                if (string.IsNullOrEmpty(productGroup.HeaderImageUrl))
                    ViewBag.headerImg = "/Images/header1.jpg";
                else
                    ViewBag.headerImg = productGroup.HeaderImageUrl;

            }


            return View(oProductGroup);
        }



        [AllowAnonymous]
        [Route("productlist/{urlParam}")]
        public ActionResult ListGrid(string urlParam)
        {
            //if (urlParam.ToLower() == "canada-consult" || urlParam.ToLower() == "schengen-consult")
            //{
            //    return RedirectPermanent("/product/" + urlParam);
            //}
            ProductGroupListViewModel oProductGroup = new ProductGroupListViewModel();
            ProductGroup productGroup = db.ProductGroups.FirstOrDefault(c => c.UrlParam == urlParam);

            if (productGroup != null)
            {

                oProductGroup.ProductGroup = productGroup;
                oProductGroup.Products = db.Products.Where(c => c.ProductGroupId == productGroup.Id && c.IsDelete == false).OrderBy(c => c.Order).ToList();
                oProductGroup.Menu = menu.ReturnMenuTours();
                oProductGroup.Footer = menu.ReturnFooter();
                oProductGroup.MenuBlogGroups = menu.ReturnBlogGroups();

                if (string.IsNullOrEmpty(productGroup.HeaderImageUrl))
                    ViewBag.headerImg = "/Images/header1.jpg";
                else
                    ViewBag.headerImg = productGroup.HeaderImageUrl;
            }
            return View(oProductGroup);
        }

        [AllowAnonymous]
        [Route("product-detail/{code:int}")]
        public ActionResult Details(int code)
        {

            Product product = db.Products.FirstOrDefault(c => c.Code == code);

            if (product == null)
            {
                return Redirect("/");
            }

            ProductDetailViewModel productDetail = new ProductDetailViewModel()
            {
                Product = product,
                SidebarProductGroups = db.ProductGroups.Where(c => c.ParentId != null && c.IsDelete == false).ToList(),
                Menu = menu.ReturnMenuTours(),
                Footer = menu.ReturnFooter(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
                RelatedProducts = db.Products.Where(c=>c.ProductGroupId==product.ProductGroupId&&c.IsDelete==false).Take(4).ToList()
            };

            if (string.IsNullOrEmpty(product.ProductGroup.HeaderImageUrl))
                ViewBag.headerImg = "/Images/header1.jpg";
            else
                ViewBag.headerImg = product.ProductGroup.HeaderImageUrl;


            ViewBag.VisaTypeId = new SelectList(db.VisaTypes.Where(c => c.IsDelete == false), "Id", "Title");
            ProductDetailPostViewModel page = new ProductDetailPostViewModel();
            ViewData.Add("Page", page);
            ViewBag.Code = product.Code;
            return View(productDetail);
        }

        [AllowAnonymous]
        [Route("Lottery")]
        public ActionResult DetailsLottery()
        {

            Product product = db.Products.FirstOrDefault(c => c.Code == 50);

            if (product == null)
            {
                return Redirect("/");
            }

            ProductDetailViewModel productDetail = new ProductDetailViewModel()
            {
                Product = product,
                SidebarProductGroups = db.ProductGroups.Where(c => c.ParentId != null && c.IsDelete == false).ToList(),
                Menu = menu.ReturnMenuTours(),
                Footer = menu.ReturnFooter(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
            };

            if (string.IsNullOrEmpty(product.ProductGroup.HeaderImageUrl))
                ViewBag.headerImg = "/Images/header1.jpg";
            else
                ViewBag.headerImg = product.ProductGroup.HeaderImageUrl;


            ViewBag.VisaTypeId = new SelectList(db.VisaTypes.Where(c => c.IsDelete == false), "Id", "Title");
            ProductDetailPostViewModel page = new ProductDetailPostViewModel();
            ViewData.Add("Page", page);
            ViewBag.Code = product.Code;
            return View(productDetail);
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("Lottery")]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsLottery(ProductDetailPostViewModel productDetailView, HttpPostedFileBase fileUpload)
        {
            int productCode = Convert.ToInt32(50);

            string cookieOrderId = GetCookieOrderId();

            Product product = db.Products.FirstOrDefault(c => c.Code == productCode);
            if (ModelState.IsValid)
            {


                if (cookieOrderId != null)
                {
                    int code = Convert.ToInt32(cookieOrderId);

                    Order oOrder =
                        db.Orders.FirstOrDefault(c => c.IsFinal == false && c.IsDelete == false && c.Code == code);

                    if (oOrder == null)
                    {
                        int orderCode = CreateNewOrderAndDetails(productDetailView, product, fileUpload);
                        db.SaveChanges();
                        SetCookieOrderId(orderCode.ToString());
                    }
                    else
                    {
                        CreateNewOrderDetail(oOrder.Id, productDetailView, product, fileUpload);

                        oOrder.TotalAmount = oOrder.TotalAmount + product.Amount;
                        oOrder.LastModificationDate = DateTime.Now;

                        db.SaveChanges();
                        SetCookieOrderId(oOrder.Code.ToString());
                    }
                }


                //CreateOrder
                else
                {

                    int orderCode = CreateNewOrderAndDetails(productDetailView, product, fileUpload);
                    db.SaveChanges();

                    SetCookieOrderId(orderCode.ToString());
                }
                return RedirectToAction("index", "Basket");

            }


            ProductDetailViewModel productDetail = new ProductDetailViewModel()
            {
                Product = product,
                SidebarProductGroups = db.ProductGroups.Where(c => c.ParentId != null && c.IsDelete == false).ToList(),
                Menu = menu.ReturnMenuTours(),
                Footer = menu.ReturnFooter(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
                RelatedProducts = db.Products.Where(c => c.ProductGroupId == product.ProductGroupId && c.IsDelete == false).Take(4).ToList()
            };

            if (string.IsNullOrEmpty(product.ProductGroup.HeaderImageUrl))
                ViewBag.headerImg = "/Images/header1.jpg";
            else
                ViewBag.headerImg = product.ProductGroup.HeaderImageUrl;


            ViewBag.VisaTypeId = new SelectList(db.VisaTypes.Where(c => c.IsDelete == false), "Id", "Title");
            ProductDetailPostViewModel page = new ProductDetailPostViewModel();
            ViewData.Add("Page", page);
            ViewBag.Code = product.Code;
            return View(productDetail);

        }

        public string GetCookieOrderId()
        {
            if (Request.Cookies["basket"] != null)
            {
                string cookievalue = Request.Cookies["basket"].Value;

                return cookievalue;
            }

            return null;
        }



        public string SetCookieOrderId(string code)
        {
            HttpContext.Response.Cookies.Set(new HttpCookie("basket")
            {
                Name = "basket",
                Value = code,
                Expires = DateTime.Now.AddDays(1)
            });

            return null;
        }

        CodeCreator codeCreator = new CodeCreator();


        [HttpPost]
        [AllowAnonymous]
        [Route("product-detail/{code:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ProductDetailPostViewModel productDetailView,HttpPostedFileBase fileUpload )
        {
            int productCode = Convert.ToInt32(productDetailView.Code);

            string cookieOrderId = GetCookieOrderId();

            Product product = db.Products.FirstOrDefault(c => c.Code == productCode);
            if (ModelState.IsValid)
            {
          

                if (cookieOrderId != null)
                {
                    int code = Convert.ToInt32(cookieOrderId);

                    Order oOrder =
                        db.Orders.FirstOrDefault(c => c.IsFinal == false && c.IsDelete == false && c.Code == code);

                    if (oOrder == null)
                    {
                        int orderCode = CreateNewOrderAndDetails(productDetailView, product, fileUpload);
                        db.SaveChanges();
                        SetCookieOrderId(orderCode.ToString());
                    }
                    else
                    {
                        CreateNewOrderDetail(oOrder.Id, productDetailView, product, fileUpload);

                        oOrder.TotalAmount = oOrder.TotalAmount + product.Amount;
                        oOrder.LastModificationDate = DateTime.Now;

                        db.SaveChanges();
                        SetCookieOrderId(oOrder.Code.ToString());
                    }
                }


                //CreateOrder
                else
                {

                    int orderCode = CreateNewOrderAndDetails(productDetailView, product, fileUpload);
                    db.SaveChanges();

                    SetCookieOrderId(orderCode.ToString());
                }
                return RedirectToAction("index", "Basket");

            }


            ProductDetailViewModel productDetail = new ProductDetailViewModel()
            {
                Product = product,
                SidebarProductGroups = db.ProductGroups.Where(c => c.ParentId != null && c.IsDelete == false).ToList(),
                Menu = menu.ReturnMenuTours(),
                Footer = menu.ReturnFooter(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
                RelatedProducts = db.Products.Where(c => c.ProductGroupId == product.ProductGroupId && c.IsDelete == false).Take(4).ToList()
            };

            if (string.IsNullOrEmpty(product.ProductGroup.HeaderImageUrl))
                ViewBag.headerImg = "/Images/header1.jpg";
            else
                ViewBag.headerImg = product.ProductGroup.HeaderImageUrl;


            ViewBag.VisaTypeId = new SelectList(db.VisaTypes.Where(c => c.IsDelete == false), "Id", "Title");
            ProductDetailPostViewModel page = new ProductDetailPostViewModel();
            ViewData.Add("Page", page);
            ViewBag.Code = product.Code;
            return View(productDetail);

        }

        public int CreateNewOrderAndDetails(ProductDetailPostViewModel productDetailView,Product product, HttpPostedFileBase fileUpload)
        {
            Order order = new Order();

            order.Id = Guid.NewGuid();
            order.IsDelete = false;
            order.IsPaid = false;
            order.SubmitDate = DateTime.Now;
            order.Code = codeCreator.ReturnOrderCode();
            order.DeliverEmail = productDetailView.Email;
            order.DeliverCellNumber = productDetailView.CellNumber;
            order.DeliverFullName = productDetailView.FirstName + "-" + productDetailView.LastName;
            order.IsFinal = false;



            decimal subTotal = product.Amount;
            order.TotalAmount = subTotal;


            db.Orders.Add(order);

            CreateNewOrderDetail(order.Id, productDetailView, product, fileUpload);

            return order.Code;
        }

        public void CreateNewOrderDetail(Guid orderId, ProductDetailPostViewModel productDetailView, Product product,HttpPostedFileBase fileUpload)
        {
            #region Upload and resize image if needed
            string newFilenameUrl = string.Empty;
            if (fileUpload != null)
            {
                string filename = Path.GetFileName(fileUpload.FileName);
                string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                     + Path.GetExtension(filename);

                newFilenameUrl = "/Uploads/Passport/" + newFilename;
                string physicalFilename = Server.MapPath(newFilenameUrl);

                fileUpload.SaveAs(physicalFilename);
            }
            #endregion

            OrderDetail orderDetail = new OrderDetail()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = 1,
                RowAmount = product.Amount,
                IsDelete = false,
                SubmitDate = DateTime.Now,
                OrderId = orderId,
                Amount = product.Amount,
                VisaTypeId = productDetailView.VisaTypeId,
                BirthDate = productDetailView.BirthDate,
                PasportNumber = productDetailView.PasportNumber,
                PasportExpireDate = productDetailView.PasportExpireDate,
                Nationality = productDetailView.Nationality,
                VisitDate1 = productDetailView.VisitDate1,
                VisitDate2 = productDetailView.VisitDate2,
                TravelDate = productDetailView.TravelDate,
                PasportFileUrl = newFilenameUrl,
                FirstName = productDetailView.FirstName,
                LastName = productDetailView.LastName,
                Email = productDetailView.Email,
                CellNumber = productDetailView.CellNumber
            };

            db.OrderDetails.Add(orderDetail);
        }
    }
}

