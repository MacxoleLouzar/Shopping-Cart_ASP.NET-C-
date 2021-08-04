using ShoppingCarty.Controllers.BusinessLogic;
using ShoppingCarty.Models;
using ShoppingCarty.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace ShoppingCarty.MVC.Controllers
{

    public class CartController : Controller
    {

        private CartBl _cart = new CartBl();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            ViewBag.CartNo = db.Carts.Count();
            if (_cart.GetAllCarts().Count == 0)
            {
                ViewBag.Msg = "Your Shopping Cart Is Empty";
            }
            else
            {
                var cart = CartBl.GetCart(this.HttpContext);
                var viewmodel = new CartItemViewModel()
                {
                    CartId = cart.GetCartId(),
                };
                //  _cart.UserItems(User.Identity.Name);
                ViewBag.Total = _cart.GetTotal().ToString("R0.00");
            }
            // CreateOrder(id,model);
            return View(_cart.GetAllCarts());
        }

        public ActionResult RemoveItem(int id)
        {
            _cart.RemoveFromCart(id);
            //  _cart.UpdateQ(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Plus_1(int id)
        {

            var cart = CartBl.GetCart(this.HttpContext);
            int itemQuantity = cart.Plus_1(id);
            cart.DecreaseQuantity(id);
            ViewData["CartCount"] = cart.GetCount();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Minus_1(int id)
        {

            var cart = CartBl.GetCart(this.HttpContext);
            int itemQuantity = cart.minus_1(id);
            cart.IncreaseQuantity(id);
            ViewData["CartCount"] = cart.GetCount();
            return RedirectToAction("Index");
        }

        //[Authorize]
        public ActionResult PlaceOrder()
        {
            string merchant_id = "10000100";
            string merchant_key = "46f0cd694581a";
            string site = "https://sandbox.payfast.co.za/eng/process?";
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));


                foreach (var x in _cart.GetAllCarts())
                {
                    str.Append("&m_payment_id=" + HttpUtility.UrlEncode(x.ItemId.ToString()));
                    str.Append("&amount=" + HttpUtility.UrlEncode(_cart.GetTotal().ToString()));
                    str.Append("&item_name=" + HttpUtility.UrlEncode("Total Amount"));


                }

                Response.Redirect(site + str.ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View("Index");
        }
    }
}