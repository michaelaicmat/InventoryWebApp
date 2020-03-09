using InventoryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.ViewModels
{
    public class CountGroupViewModel
    {
        public bool ShowNoRowAlert { get; set; }
        public List<GroupRecord> GroupRecords { get; set; }

        public CountGroupViewModel()
        {
            GroupRecords = new List<GroupRecord>();
        }
    }
}