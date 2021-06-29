using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$",
          ErrorMessage ="Bad input no special charters and numbers not allowed"  )]
        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }
        
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$",
          ErrorMessage = "Bad input no special charters and numbers not allowed")]
        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }


        [StringLength(13, MinimumLength = 5)]
        [Required]
        public string PhoneNumber { get; set; }
        

        [DisplayName("[BookableHours-ID]")]
        public int BookableHoursID { get; set; }

        public BookableHours BookableHours { get; set; }
    }
}
