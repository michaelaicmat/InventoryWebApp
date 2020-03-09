using InventoryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.ViewModels
{
    public class InventoryViewModel
    {
        public List<InventoryRecord> Records { get; set; }
        public InventoryRecord SelectedRecord { get; set; }
        public bool ShowNoRowAlert { get; set; }

        public InventoryViewModel()
        {
            Records = new List<InventoryRecord>();
        }
    }
}