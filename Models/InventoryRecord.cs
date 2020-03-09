using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Models
{
    public class InventoryRecord
    {
        public string ctgp { get; set; } //count group
        public string whid { get; set; } //warehouse id 
        public string itno { get; set; } //item number
        public string itdsc { get; set; } //item description
        public string blno { get; set; } //batch or lot
        public string whlc { get; set; } //location
        public string unmsr { get; set; } //stocks UM
        public string bdentry { get; set; } //bundles
        public string seentry { get; set; } //sections
        public string stkentry { get; set; } //stocks
        public string updatedby { get; set; } //Updated By User
        public string updated { get; set; } //stocks


        public string um2bd { get; set; } //stocks
        public string bdqnty { get; set; } //stocks
        public string bdconvert { get; set; } //stocks


        public string um2se { get; set; } //stocks
        public string seqnty { get; set; } //stocks
        public string seconvert { get; set; } //stocks

        public string umbd { get; set; } //stocks
        public string umse { get; set; } //stocks

        public string ctq1 { get; set; } //stocks
       
       
        
       
    }
}