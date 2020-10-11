using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eshop.Helpers;
using Helpers;
using Models;
using ViewModels;

namespace Bisan_New.Controllers
{
    public class BasketController : Controller
    {
        MenuHelper menu = new MenuHelper();
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            CartViewModel cart = new CartViewModel();

            cart.Menu = menu.ReturnMenuTours();
            cart.Footer = menu.ReturnFooter();
            cart.MenuBlogGroups = menu.ReturnBlogGroups();

            string orderCode = GetCookieOrderId();

            if (!string.IsNullOrEmpty(orderCode))
            {
                int code = Convert.ToInt32(orderCode);

                Order order = db.Orders.FirstOrDefault(c => c.Code == code);

                if (order != null)
                {
                    cart.Total = order.TotalAmountStr;
                    cart.Products = GetProductInBasketByCoockie(order);
                    cart.MID = "10932853";
                    cart.RedirectURL = "https://www.bektashtravel.com/payment/callback";
                    cart.Amount = (order.TotalAmount * 10).ToString().Split('/')[0];
                    cart.ResNum = orderCode;
                }
            }


         
            return View(cart);
        }


        #region newMethods

        public List<ProductInCart> GetProductInBasketByCoockie(Order order)
        {
            List<ProductInCart> productInCarts = new List<ProductInCart>();

            List<OrderDetail> orderDetails =
                db.OrderDetails.Where(c => c.OrderId == order.Id && c.IsDelete == false).ToList();


            foreach (OrderDetail orderDetail in orderDetails)
            {
                Product product =
                    db.Products.FirstOrDefault(current =>
                        current.IsDelete == false && current.Id == orderDetail.ProductId);

                productInCarts.Add(new ProductInCart()
                {
                    OrderDetailId = orderDetail.Id,
                    Product = product,
                    Quantity = 1,
                    FullName = orderDetail.FirstName+" - "+orderDetail.LastName
                });
            }

            return productInCarts;
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

        #endregion

        public decimal GetSubtotal(List<ProductInCart> orderDetails)
        {
            decimal subTotal = 0;

            foreach (ProductInCart orderDetail in orderDetails)
            {
                decimal amount = orderDetail.Product.Amount;

                subTotal = subTotal + (amount * orderDetail.Quantity);
            }

            return subTotal;
        }

        public List<ProductInCart> GetProductInBasketByCoockie()
        {
            List<ProductInCart> productInCarts = new List<ProductInCart>();

            string[] basketItems = GetCookie();

            if (basketItems != null)
            {
                for (int i = 0; i < basketItems.Length - 1; i++)
                {


                    int productCode = Convert.ToInt32(basketItems[i]);

                    Product product =
                        db.Products.FirstOrDefault(current =>
                            current.IsDelete == false && current.Code == productCode);

                    productInCarts.Add(new ProductInCart()
                    {
                        Product = product,
                        Quantity = 1,
                    });
                }
            }

            return productInCarts;
        }


        public string[] GetCookie()
        {
            if (Request.Cookies["basket"] != null)
            {
                string cookievalue = Request.Cookies["basket"].Value;

                string[] basketItems = cookievalue.Split('/');

                return basketItems;
            }

            return null;
        }




        [AllowAnonymous]
        public ActionResult Finalize(string cellnumber, string email, string fullname)
        {
            try
            {
                cellnumber = cellnumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3")
                    .Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8")
                    .Replace("۹", "9");


                List<ProductInCart> productInCarts = GetProductInBasketByCoockie();

                Order order = ConvertCoockieToOrder(productInCarts, cellnumber, email, fullname);

                if (order != null)
                {


                    RemoveCookie();

                    string rialAmount = (order.TotalAmount * 10).ToString().Split('/')[0];

                    string[] res = new string[] { order.Code.ToString(), rialAmount };


                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                return Json("false", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public Guid CreateUser(string cellnumber, string email, string fullname)
        {
            Role role = db.Roles.FirstOrDefault(c => c.Name == "customer");
            User oUser = new User()
            {
                FullName = fullname,
                Username = cellnumber,
                Id = Guid.NewGuid(),
                SubmitDate = DateTime.Now,
                IsDelete = false,
                Password = RandomCode(),
                Email = email,
                RoleId = new Guid("7A01F572-1ED7-46AA-9FA2-2D5AD1AA838A"),
                LastModificationDate = DateTime.Now
            };

            db.Users.Add(oUser);
            db.SaveChanges();

            return oUser.Id;
        }

        public string RandomCode()
        {
            Random generator = new Random();
            String r = generator.Next(0, 100000).ToString("D5");
            return r;
        }


        public void RemoveCookie()
        {
            if (Request.Cookies["basket"] != null)
            {
                Response.Cookies["basket"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        public Order ConvertCoockieToOrder(List<ProductInCart> products, string cellnumber, string email, string fullname)
        {
            try
            {
                CodeCreator codeCreator = new CodeCreator();

                Order order = new Order();

                order.Id = Guid.NewGuid();
                order.IsDelete = false;
                order.IsPaid = false;
                order.SubmitDate = DateTime.Now;
                order.Code = codeCreator.ReturnOrderCode();
                order.DeliverEmail = email;
                order.DeliverCellNumber = cellnumber;
                order.DeliverFullName = fullname;

                decimal subTotal = GetSubtotal(products);
                order.TotalAmount = subTotal;


                db.Orders.Add(order);
                db.SaveChanges();




                foreach (ProductInCart product in products)
                {
                    decimal amount = product.Product.Amount;



                    OrderDetail orderDetail = new OrderDetail()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Product.Id,
                        Quantity = product.Quantity,
                        RowAmount = amount * product.Quantity,
                        IsDelete = false,
                        SubmitDate = DateTime.Now,
                        OrderId = order.Id,
                        Amount = amount
                    };

                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();

                }

                return order;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        [Route("Basket/remove/{orderDetailId:Guid}")]
        public ActionResult RemoveFromBasket(Guid orderDetailId)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(orderDetailId);

            if (orderDetail != null)
            {
                orderDetail.IsDelete = true;
                orderDetail.DeleteDate=DateTime.Now;
                orderDetail.LastModificationDate=DateTime.Now;


                Order order = db.Orders.FirstOrDefault(c => c.Id == orderDetail.OrderId);

                if (order != null)
                {
                    order.TotalAmount = order.TotalAmount - orderDetail.RowAmount;
                    order.LastModificationDate=DateTime.Now;
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}