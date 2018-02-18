using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//ne koristi se 
namespace PixelBin.Models
{
    public class Administrator : User
    {
        public List<Image> pinnedImages { get; set; }

        public Administrator(Guid adminId, string name, string surname) : base(adminId, name, surname)
        {
            //
            pinnedImages=new List<Image>();
        }

        public Administrator()
        {
            
        }

    }
}
