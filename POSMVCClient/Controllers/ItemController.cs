using POSMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace POSMVCClient.Controllers
{
    public class ItemController : Controller
    {
        public static List<ServiceReference1.Items> itemsList = new List<ServiceReference1.Items>();
        public static string itemAvailability = "";

        // GET: Item
        public ActionResult Index()
        {

            ViewBag.message = itemAvailability;
            return View();
        }


        [HttpPost]
        public ActionResult CheckItemAvailability(String ItemId) //Variable name should be same as field name
        {
            itemAvailability = "";
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            ServiceReference1.Items items = sc.GetItemDetails(ItemId);

            if(null == items)

            {


                //return Content("<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>");
                //ViewBag.Javascript = "Data not available";
                itemAvailability = "Item not available";
                return RedirectToAction("Index");

            }

            else
            {
                itemsList.Add(items);


                return RedirectToAction("Index", "Item");

            }
    
            
        }

        public ActionResult ItemIdTextBox()
        {
            return PartialView();
        }

        public ActionResult DisplayItemList()
        {

            return PartialView(itemsList);

        }

        //public ActionResult DeleteItem()
        //{

        //    itemsList.
        //}

        public ActionResult FinishBilling()
        {
            itemsList.Clear();
            itemAvailability = "";


            return RedirectToAction("Index", "Item");
        }
    }
}