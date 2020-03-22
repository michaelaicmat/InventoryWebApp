using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace InventoryWebApp.Models
{
    public class User 
    {
        public string EmailAddress { get; set; }
    
        public string Password { get; set; }
    }
}