using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShowApplication.Models
{
    public class Show
    {
        [Key]
        public int showID { get; set; }

       //foreign key for artist
        [ForeignKey("artist")]
        public int artistID { get; set; }
        public virtual Artist artist { get; set; }

        //foreign key for venueID
        [ForeignKey("venue")]
        public int venueID { get; set; }
        public virtual Venue venue { get; set; }

        public DateTime DateAndTime { get; set; }


       
    }
}