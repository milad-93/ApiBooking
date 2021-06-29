
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooking.Models
{
    public class BookableHours
    {
        public int Id { get; set; }
          [Display(Name = "Booking start"), DataType(DataType.DateTime)]
       // [Required, Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime StartTime { get; set; }

        [Display(Name = "Booking End "), DataType(DataType.DateTime)]
        //[Required, Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public   DateTime EndTime { get; set; }


        //relationShip
        public virtual IList<Booking> Person { get; set; } // list of bookings 
       



    }
}
