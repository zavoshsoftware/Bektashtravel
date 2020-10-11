using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bisan_New.Controllers
{
    public class RedirectController : Controller
    {
     
        [Route("destination/{id:Guid}")]
        public ActionResult Redirect3(Guid id)
        {
            return RedirectPermanent("/");
        }

        [Route("travel/destination/{id:Guid}")]
        public ActionResult Redirect4(Guid id)
        {
            return RedirectPermanent("/");
        }

        [Route("destination/List/{id:Guid}")]
        public ActionResult Redirect5(Guid id)
        {
            return RedirectPermanent("/");
        }
    }
}