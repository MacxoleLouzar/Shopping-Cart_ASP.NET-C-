using ShoppingCarty.Controllers.BusinessLogic;
using ShoppingCarty.Models;
using ShoppingCarty.Models.Entity;
using ShoppingCarty.Models.ViewModel;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace ShoppingCarty.MVC.Controllers
{
    public class ItemController : Controller
    {
        private ItemBl _item = new ItemBl();
        private readonly CartBl _cart = new CartBl();
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Item
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            return View(_item.GetAll());
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost, ActionName("AddItem")]
        public ActionResult AddItem(ItemViewModel model, HttpPostedFileBase upload)
        {

            _item.AddNewItem(model, upload);
            return RedirectToAction("Index");


        }

        public ActionResult AddToCart(int id, string sizeList)
        {
        
            _cart.AddToCart(id);
            _cart.SubTot(id);

            _cart.DecreaseQuantity(id);
            ViewBag.Total = _cart.GetTotal();
            return RedirectToAction("Index", "Cart");
           
        }

        public ActionResult CartItem()
        {
            return View(_item.GetAll());
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item  = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Image,Name,Description,YearReleased,TotQuantity,ItemPrice")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}