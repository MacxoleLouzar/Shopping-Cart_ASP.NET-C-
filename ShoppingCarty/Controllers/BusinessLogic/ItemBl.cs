
using ShoppingCarty.Models;
using ShoppingCarty.Models.Entity;
using ShoppingCarty.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCarty.Controllers.BusinessLogic
{

    public class ItemBl
    {
        private ApplicationDbContext _db = new ApplicationDbContext();


        public void AddNewItem(ItemViewModel model, HttpPostedFileBase upload)
        {
            var fileLength = upload.ContentLength;
            byte[] ImageBytes = new byte[fileLength];
            upload.InputStream.Read(ImageBytes, 0, fileLength);

            var item = new Item
            {
                ItemId = model.ItemId,
                Image = ImageBytes,
                Name = model.Name,
                Description = model.Description,
                ItemPrice = model.ItemPrice,
                YearReleased = model.YearReleased,
                TotQuantity = model.Quantity

            };
            _db.Items.Add(item);
            _db.SaveChanges();

        }
        public List<ItemViewModel> GetAll()
        {

            return _db.Items.ToList().Select(x => new ItemViewModel()
            {
                ItemId = x.ItemId,
                Image = x.Image,
                Name = x.Name,
                Description = x.Description,
                ItemPrice = x.ItemPrice,
                YearReleased = x.YearReleased,
                Quantity = x.TotQuantity
            }).ToList();
        }
    }
}