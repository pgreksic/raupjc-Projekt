using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBin.Models
{
    public class Dislike
    {
        public Guid DislikeId { get; set; }
        //public Guid imageId { get; set; }
        public Guid UserId { get; set; }

        //Guid imageId, 
        public Dislike(Guid userId)
        {
            DislikeId = new Guid();
            //this.imageId = imageId;
            this.UserId = userId;
        }

        public Dislike()
        {

        }

        //overridana equals metoda
        public override bool Equals(object obj)
        {
            var item = obj as Dislike;

            if (item == null)
            {
                return false;
            }

            return this.DislikeId.Equals(item.DislikeId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.DislikeId.GetHashCode();
        }
    }
}
