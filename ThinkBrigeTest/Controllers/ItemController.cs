using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using ManagerLayer;

namespace ThinkBrigeTest.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetData()
        {
            ItemML ML = new ItemML();
            List<ItemBL> itemList = ML.ListAllItem().ToList<ItemBL>();
            var listWithoutCol = itemList.Select(x => new { x.Name, x.Price,x.ID }).ToList();
            return Json(new { data = listWithoutCol }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetById(string Id)
        {
            ItemML ML = new ItemML();
            List<ItemBL> itemList = ML.ListAllItem().ToList<ItemBL>();
            var result = itemList.Find(s => s.ID.Equals(Id));
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddEditItem(string Name, string Desc, double Price, string Id)
        {
            ItemML ML = new ItemML();
            if (Id == "" || Id == null || Id == string.Empty)
            {
                ML.InsertItem(Name, Desc, Price);
            }
            else
            {
                Guid GuidId = Guid.Parse(Id);
                ML.UpdateItem(Name, Desc, Price, GuidId);
            }

            return Json("Success");
        }

        [HttpPost]
        public ActionResult ItemDeleteById(string Id)
        {
            ItemML ML = new ItemML();
            Guid GuidId = Guid.Parse(Id);
            ML.DeleteItem(GuidId);
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

    }
}