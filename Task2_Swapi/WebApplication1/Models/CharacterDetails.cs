using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CharacterDetails :Character
    {
        public string Homeworld { get; set; }
        public int Eye_color { get; set; }
        public int Skin_color { get; set; }
        public int Mass { get; set; }
        public List<string> Films { get; set; }
        public List<string> Species { get; set; }
        public List<string> Vehicles { get; set; }
        public List<string> Starships { get; set; }
        public DateTime Edited { get; set; }
        public DateTime Created { get; set; }
    }
}