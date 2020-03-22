using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Models
{
    public class ContactViewModel

    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Message")]
        [StringLength(100, ErrorMessage = "The message was not delivered. Please try again later.")]
        public string Message { get; set; }


    }
}