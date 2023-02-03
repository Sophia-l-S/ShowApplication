using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowApplication.Models
{
    public class Artist
    {
        [Key]
        public int ArtistlID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }


    }
    public class ArtistDto
    {
        public int ArtistlID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }



    }



}