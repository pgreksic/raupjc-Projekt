using System;

namespace PixelBin.Models
{
    public class Like
    {
        public Guid LikeId { get; set; }
        //public Guid imageId { get; set; }
        public Guid UserId { get; set; }

        //Guid imageId, 
        public Like(Guid userId)
        {
            LikeId=new Guid();
            //this.imageId = imageId;
            this.UserId = userId;
        }

        public Like()
        {
            
        }

        //overridana equals metoda
        public override bool Equals(object obj)
        {
            var item = obj as Like;

            if (item == null)
            {
                return false;
            }

            return this.LikeId.Equals(item.LikeId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.LikeId.GetHashCode();
        }
    }
}