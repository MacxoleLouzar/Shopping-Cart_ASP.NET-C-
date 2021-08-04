using ShoppingCarty.Models;
using ShoppingCarty.Models.Entity;
using ShoppingCarty.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShoppingCarty.Controllers.BusinessLogic

{
    public class CartBl : IDisposable
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        //   private readonly CartRepository _rep = new CartRepository();
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static CartBl GetCart(HttpContextBase context)
        {
            var cart = new CartBl();
            cart.ShoppingCartId = cart.GetCartId();
            return cart;
        }
        public static CartBl GetCart(Controller controller)
        {

            return GetCart(controller.HttpContext);
        }

        public List<CartItemViewModel> GetAllCarts()
        {
            ShoppingCartId = GetCartId();

            return _db.Carts.ToList().Where(i => i.UserId == ShoppingCartId).Select(x => new CartItemViewModel()
            {
                CartId = x.CartId,
                UserId = x.UserId,
                ItemId = x.ItemId,
                ItemName = x.Item.Name,
                DateCreated = x.DateCreated,
                Quantity = x.Quantity,
                Image = x.Item.Image,
                ItemPrice = (int)x.Item.ItemPrice,
                TotalPrice = GetTotal(),
                SubTotal = SubTot(x.ItemId),
                TotQ = x.Item.TotQuantity


            }).ToList();
        }

        public void AddToCart(int id)
        {
            ShoppingCartId = GetCartId();
            //  var cartItems = rep.GetById(id);
            //  var cartItems = db.Carts.ToList().Find(x => x.ItemId == id);
            var cartItems = _db.Carts.ToList().SingleOrDefault(x => x.UserId == ShoppingCartId && x.ItemId == id);

            if (cartItems == null)
            {
                cartItems = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    ItemId = id,
                    UserId = ShoppingCartId,
                    Item = _db.Items.SingleOrDefault(p => p.ItemId == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.Carts.Add(cartItems);
            }
            else
            {
                cartItems.Quantity++;

            }
            _db.SaveChanges();

        }

        //public int UpdateQuantity(int id)
        //{
        //    var cartqty=_db.
        //    if (AddToCart(id) != null)
        //    {
        //        cartqty=new Cart
        //        {
        //            Quantity = 
        //        }
        //    }
        //}


        public int minus_1(int id)
        {
            ShoppingCartId = GetCartId();
            //var cartItems = db.Carts.Single(x => x.UserId == ShoppingCartId && x.ItemId == id);
            var cartItems = _db.Carts.ToList().Find(x => x.ItemId == id);
            int itemQuantity = 0;
            //Debug.Assert(cartItems != null, "cartItems != null");
            if (cartItems.Quantity > 1)
            {
                cartItems.Quantity--;
                itemQuantity = cartItems.Quantity;
            }
            else
            {
                _db.Carts.Remove(cartItems);
            }
            _db.SaveChanges();
            return itemQuantity;
        }

        public void DecreaseQuantity(int id)
        {
            var cartItems = _db.Items.ToList().Find(x => x.ItemId == id);
            if (cartItems != null)
            {
                cartItems.TotQuantity--;
            }
            _db.SaveChanges();
        }

        public void UpdateQ(int id)
        {
            var cartItems = _db.Items.ToList().Find(x => x.ItemId == id);
            var cartItem = _db.Carts.ToList().Find(x => x.ItemId == id);
            if (cartItems != null)
            {

            }
            _db.SaveChanges();

        }
        public void IncreaseQuantity(int id)
        {
            var cartItems = _db.Items.ToList().Find(x => x.ItemId == id);
            if (cartItems != null)
            {
                cartItems.TotQuantity++;
            }
            _db.SaveChanges();
        }

        public void RemoveFromCart(int id)
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.Carts.Single(x => x.UserId == ShoppingCartId && x.ItemId == id);

            //int itemQuantity = 0;
            if (cartItems != null)
            {
                _db.Carts.Remove(cartItems);
                _db.SaveChanges();
            }
            //   return itemQuantity;
        }

        public int Plus_1(int id)
        {

            ShoppingCartId = GetCartId();
            var cartItems = _db.Carts.Single(x => x.UserId == ShoppingCartId && x.ItemId == id);
            int itemQuantity = 0;
            if (cartItems != null)
            {
                cartItems.Quantity++;
                itemQuantity = cartItems.Quantity;
                //Itemqty++;
                _db.SaveChanges();
            }
            return itemQuantity;
        }


        public int GetCount()
        {
            int? count = (from cartItem in _db.Carts
                          where cartItem.CartId == ShoppingCartId
                          select (int?)
                              cartItem.Quantity).Sum();

            return count ?? 0;
        }

        //public int GetCount()
        //{
        //    int? count =(from Cart in db.Carts where Cart.CartId==ShoppingCartId select (int?) Cart.Quantity.Sum())
        //}
        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();

            return (from x in _db.Carts
                    where x.UserId == ShoppingCartId
                    select x.Quantity * x.Item.ItemPrice).Sum();

        }

        public decimal SubTot(int id)
        {
            ShoppingCartId = GetCartId();
            return (from x in _db.Carts
                    where x.UserId == ShoppingCartId && x.ItemId == id
                    select x.Quantity * (int)x.Item.ItemPrice).Sum();
        }

        //user's item
        public List<CartItemViewModel> UserItems(string user)
        {
            //  ShoppingCartId = GetCartId();

            return _db.Carts.ToList().Where(i => i.UserId == user).Select(x => new CartItemViewModel()
            {
                CartId = x.CartId,
                UserId = x.UserId,
                ItemId = x.ItemId,
                ItemName = x.Item.Name,
                DateCreated = x.DateCreated,
                Quantity = x.Quantity,
                Image = x.Item.Image,
                ItemPrice = (int)x.Item.ItemPrice,
                TotalPrice = GetTotal(),
                SubTotal = SubTot(x.ItemId)

            }).ToList();
        }

        public void Migration(string cartId, string userId)
        {
            foreach (Cart x in _db.Carts.Where(x => x.CartId == cartId))
            {
                x.CartId = userId;
            }
            HttpContext.Current.Session[CartSessionKey] = userId;
            _db.SaveChanges();
        }

        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }

        }
    }
}
