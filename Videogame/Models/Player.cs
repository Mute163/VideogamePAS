using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Videogame.Models
{
    public class Player
    {
        [Required]
        public int PlayerId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string PlayerName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string PlayerLastName { get; set; }


        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [StringLength(30)]
        public string Nationality { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string email { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }
}
