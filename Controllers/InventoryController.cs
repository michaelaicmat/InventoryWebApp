using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InventoryWebApp.Models;
using System.Data;
using System.Data.SqlClient;
using InventoryWebApp.Infrastructure.DAL.Services;
using InventoryWebApp.ViewModels;
using System.Linq;

namespace InventoryWebApp.Controllers
{

    //Check if group doesnt have any item, and displays a pop up.
    //Return to the same window, else redirect.
    [Authorize]
    public class InventoryController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string groupId)
        {
            var inventoryItems = InventoryDataService.GetInventoryRecordsByGroupId(groupId);

            if(inventoryItems.Count == 0)
            {
                return RedirectToAction("Index", "Group", new { showNoRowAlert = true });
            }
            ViewBag.iD = groupId;
            return View("Inventory", new InventoryViewModel() {
                
                Records = inventoryItems,
                SelectedRecord = inventoryItems.First()

            });
           
        }

        [HttpPost]
        public ActionResult SearchInventory(string inventoryId, string groupId)
        {

            var inventoryItem = InventoryDataService.GetInventoryRecordById(inventoryId, groupId);
            var inventoryItems = InventoryDataService.GetInventoryRecordsByGroupId(groupId);
            ViewBag.iD = groupId;

            if (inventoryItem.Count == 0)
            {
                return View("Inventory", new InventoryViewModel()
                {

                    Records = inventoryItems,
                    SelectedRecord = inventoryItems.First()

                });
            }
            return View("Inventory", new InventoryViewModel()
            {
                Records = inventoryItems,
                SelectedRecord = inventoryItem.First()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectInventory(string countGroup, string itemNumber, string warehouseLoc, string batchLot)
        {
            var inventoryItems = InventoryDataService.GetInventoryRecordsByGroupId(countGroup);
            ViewBag.iD = countGroup;
            return View("Inventory", new InventoryViewModel()
            {
                Records = inventoryItems,
                SelectedRecord = InventoryDataService.SelectInventory(countGroup, itemNumber, warehouseLoc, batchLot).First()
            });
        }

        [HttpPost]
        public ActionResult UpdateInventory(string itno, string bdentry, string seentry, string stkentry, string ctgp, string blno, string whlc)
        {
            var inventoryItems = InventoryDataService.UpdateInventory(itno, bdentry, seentry, stkentry, ctgp, blno, whlc);
            var inventoryItems2 = InventoryDataService.GetInventoryRecordsByGroupId(ctgp);

            if (inventoryItems.Count == 1)
            {
                return RedirectToAction("Index", "Group");
            }

            return View("Inventory", new InventoryViewModel()
            {
                Records = inventoryItems2,
                SelectedRecord = InventoryDataService.SelectInventory(ctgp, itno, whlc, blno).First()
            });
        }
    }
}