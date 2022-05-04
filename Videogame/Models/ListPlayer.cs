using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Videogame.Models
{
    public class ListPlayer
    {
        public List<Player> Players  { get; set; }
        public SelectList Nationality { get; set; }
        public string PlayerNationality { get; set; }

    }
}
