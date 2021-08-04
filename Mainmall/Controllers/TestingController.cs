using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCarty.Controllers.BusinessLogic;

namespace Mainmall.Controllers
{
    public class TestingController : Controller
    {
        ItemBl ib = new ItemBl();
        // GET: Testing
      
        public ActionResult Index() { 
            return RedirectToAction("CartItem","Item");
        }
    }
}