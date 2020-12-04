using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domaci1
{
    public class FakultetViewModel
    {
        public int BrIndeksa { get; set; }
        public Nullable<int> IdS { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Grad { get; set; }
    }
}