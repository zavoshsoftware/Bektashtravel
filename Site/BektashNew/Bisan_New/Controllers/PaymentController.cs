using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bisan_New.ir.shaparak.sep;
using Helpers;
using Models;
using ViewModels;

namespace Bisan_New.Controllers
{


    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index(string am, string code)
        {
            paymentViewModel pvm = new paymentViewModel()
            {
                Amount = am,
                ResNum = code,
                MID = "10932853",
                RedirectURL = "https://www.bektashtravel.com/payment/callback"
            };
            t_lAmount = Convert.ToDouble(am);
            return View(pvm);
        }
        public double t_lAmount = 0;
        public string message = string.Empty;
        public string t_strRefNum = string.Empty;
        public string t_strResNum = string.Empty;
        public ActionResult CallBack()
        {
            #region fields
            //    t_lAmount = double.Parse(Session["Amount"].ToString());
            bool isError = false;
            string strMsg = string.Empty;
            double Result = -1000;



            // وب سرویس بانک سامان
            /*=>*/
            ir.shaparak.sep.PaymentIFBinding samanBankServices = new PaymentIFBinding();
            //  com.sb24.acquirer.ReferencePayment samanBankServices = new com.sb24.acquirer.ReferencePayment();
            #endregion
            if (Request.Form["State"].Equals("OK"))
            {
                t_strRefNum = Request.Form["RefNum"].ToString();
                t_strResNum = Request.Form["ResNum"].ToString();

                Order order = GetOrderByCode(t_strResNum);

                if (order != null)
                    t_lAmount = (double)(order.TotalAmount * 10);
                else
                    t_lAmount = 0;

                if (t_strRefNum.Equals(string.Empty))
                {
                    isError = true;
                    strMsg = "گويا خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت";
                    t_strResNum = "مشکلي در فرايند رزرو خريد شما پيش آمده است";
                    message = strMsg;
                }
                else
                {
                    Session["RefNum"] = Request.Form["RefNum"].ToString();
                    try
                    {
                        string MID = "10932853";
                        Result = samanBankServices.verifyTransaction(t_strRefNum, MID);
                        double strTempRes = Result;
                        string strNodeType;
                        if (strTempRes > 0)
                        {
                            strTempRes = 1;
                        }
                        switch ((int)strTempRes)
                        {
                            case 1:     //VERIFIED
                                        //connection.Open()
                                if (Result < t_lAmount) //Total Amount of Basket
                                {
                                    strMsg = "مبلغ انتقالي کمتر از مبلغ کل فاکتور ميباشد";
                                    isError = true;
                                }
                                else
                                    if (Result.Equals(t_lAmount)) //Total Amount of Basket
                                {
                                    isError = false;
                                    strMsg = "پرداخت شما با موفقیت انجام شد";
                                    ChangeOrderPayment(t_strResNum, t_strRefNum);
                                    ViewBag.res = Result;
                                }
                                else
                                        if (Result > t_lAmount) //Total Amount of Basket
                                {
                                    isError = false;
                                    strMsg = "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت.";

                                    // strMsg = string.Format("خريد شما تاييد و نهايي گشت اما مبلغ انتقالي {0} ريال بيش از مبلغ خريد ميباشد", Result);
                                    ChangeOrderPayment(t_strResNum, t_strRefNum);
                                }
                                break;
                            case -1:        //TP_ERROR
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -2:        //ACCOUNTS_DONT_MATCH
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "بروز خطا در هنگام تاييد رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -3:        //BAD_INPUT
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -4:        //INVALID_PASSWORD_OR_ACCOUNT
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -5:        //DATABASE_EXCEPTION
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -7:        //ERROR_STR_NULL
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -8:        //ERROR_STR_TOO_LONG
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -9:        //ERROR_STR_NOT_AL_NUM
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -10:   //ERROR_STR_NOT_BASE64
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            case -11:   //ERROR_STR_TOO_SHORT
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                break;
                            default:
                                isError = false;
                                /*
                                isError = true;
                                strNodeType = "errornode";
                                strMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                 * */
                                break;
                        }

                        message = strMsg;
                    }
                    catch (Exception ex)
                    {
                        isError = true;
                        strMsg = "سرور بانک براي تاييد رسيد ديجيتالي در دسترس نيست<br><br><div dir ='ltr' align='left'>" + ex.Message + "</div>";
                        message = strMsg;

                    }
                }
                if (t_strRefNum.Equals(string.Empty))
                {
                    isError = true;
                    strMsg = "گويا فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                    t_strRefNum = "مشکلي در خريد شما پيش آمده است";
                    message = strMsg;

                }
                if (isError)
                    message = string.Format("<P><font color=\"#DD0000\">{0}{1}</font></P>", strMsg, Request.Form["State"]);
                else
                    message = strMsg;
            }
            else
            {
                strMsg = string.Format("{0} متاسفانه بانک خريد شما را تاييد نکرده است", Request.Form["State"]);
                isError = true;
                t_strRefNum = "خريد شما تاييد نگشته است";
                t_strResNum = string.Format("ديگر معتبر نيست {0} شماره خريد", Request.Form["ResNum"]);
                message = strMsg;
            }
            //update database
            int t_intState;
            if (isError)
                t_intState = 2;
            else
                t_intState = 1;
            if (Request.Form["ResNum"].Equals(string.Empty))
            {
                strMsg = "خطا در برقرار ارتباط با بانک";
                isError = false;
                message = strMsg;

            }
            else
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("RefNum", Request.Form["RefNum"]);
                parameters.Add("State", t_intState);
                parameters.Add("ResNum", Request.Form["ResNum"]);
                // message = "RefNum=" + Request.Form["RefNum"]+ "/State="+ t_intState+ "/ResNum"+ Request.Form["ResNum"];

                /*=>*/

            }

            MenuHelper menu = new MenuHelper();

            CallBackViewMode cl = new CallBackViewMode()
            {
                Menu = menu.ReturnMenuTours(),
                Footer = menu.ReturnFooter(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
                Message = message,
                Result = Result,
                IsSuccess = !isError,
                RefId = Request.Form["RefNum"]
            };
            RemoveCookie();

            if (!string.IsNullOrEmpty(t_strResNum))
            {
                Order oOrder = GetOrderByCode(t_strResNum);

                if (oOrder != null)
                {
                    CreateSucessMessage(oOrder.DeliverCellNumber, oOrder.DeliverFullName);
                    CreateAdminSucessMessage(oOrder.DeliverCellNumber, oOrder.TotalAmountStr);
                }
            }
            return View(cl);
        }

        //[HttpPost]
        //public ActionResult Index(paymentViewModel payment)
        //{
        //    paymentViewModel pvm=new paymentViewModel()
        //    {
        //        Amount = "1000",
        //        ResNum = "1",
        //        MID= "10932853",
        //        RedirectURL = "https://google.com"
        //    };
        //    return View(pvm);
        //}
        private DatabaseContext db = new DatabaseContext();

        public void RemoveCookie()
        {
            if (Request.Cookies["basket"] != null)
            {
                Response.Cookies["basket"].Expires = DateTime.Now.AddDays(-1);
            }
        }


        public void ChangeOrderPayment(string orderCode, string refId)
        {
            int code = Convert.ToInt32(orderCode);

            Order order = db.Orders.FirstOrDefault(c => c.Code == code);

            if (order != null)
            {
                order.IsPaid = true;
                order.PaymentDate = DateTime.Now;
                order.IsFinal = true;
                order.RefId = refId;
            }

            db.SaveChanges();
        }
        public Order GetOrderByCode(string orderCode)
        {
            int code = Convert.ToInt32(orderCode);

            Order order = db.Orders.FirstOrDefault(c => c.Code == code);

            if (order != null)
            {
                return order;

            }
            return null;
        }



        public void CreateSucessMessage(string cellNumber, string fullName)
        {
            Sms sms = new Sms();
            string nextLine = "\n";

            string message = fullName + " عزیز" + nextLine + "با تشکر از خرید شما از وب سایت بکتاش سیر گشت " + nextLine +
                " کارشناسان ما جهت هماهنگی با شما طی ۲۴ ساعت آینده تماس خواهند گرفت";
            sms.SendCommonSms(cellNumber, message);
        }


        public void CreateAdminSucessMessage(string cellNumber, string amount)
        {
            Sms sms = new Sms();

            string message = "یک سفارش به مبلغ " + amount + " در وبسایت بکتاش سیر گشت ثبت شد";
            sms.SendCommonSms(cellNumber, message);
        }
    }

    public class paymentViewModel
    {
        public string ResNum { get; set; }
        public string Amount { get; set; }
        public string MID { get; set; }
        public string RedirectURL { get; set; }
    }
}