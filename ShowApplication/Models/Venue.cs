using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShowApplication.Models
{
    public class Venue
    {
        [Key]
        public int SpeciesID { get; set; }

        public string venueName { get; set; }
        public string City { get; set; }
        public string ProvenceState { get; set; }
       
    }
}