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

            return View("Inventory", new InventoryViewModel() {
                Records = inventoryItems,
                SelectedRecord = inventoryItems.First()
            });
        }

        [HttpPost]
        public ActionResult SearchInventory(string inventoryId)
        {
            //Should search item with the smae inventoryId and countGroup from previous page
            //Dropdown shld stay the same
            // on search, form shld be populated of what u searched
            var inventoryItems = InventoryDataService.GetInventoryRecordById(inventoryId);
            
          
            if (inventoryItems.Count == 1)
            {
                return RedirectToAction("Index", "Group");
            }

            return View("Inventory", new InventoryViewModel()
            {
                Records = inventoryItems,
                SelectedRecord = inventoryItems.First()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectInventory(string countGroup, string itemNumber, string warehouseLoc, string batchLot)
        {
            var inventoryItems = InventoryDataService.GetInventoryRecordsByGroupId(countGroup);

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