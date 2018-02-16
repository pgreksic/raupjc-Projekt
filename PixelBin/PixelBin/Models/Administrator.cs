using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBin.Models
{
    public class Administrator : User
    {
        public List<Image> pinnedImages { get; set; }

        public Administrator(string name, string surname) : base(name, surname)
        {
            //
            pinnedImages=new List<Image>();
        }

        public Administrator()
        {
            
        }

    }
}
