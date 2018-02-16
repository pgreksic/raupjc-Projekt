using System;
using System.Collections.Generic;
using Microsoft.Isam.Esent.Interop.Server2003;

namespace PixelBin.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public Guid ImageOwnerId { get; set; }
        public string ImageDescription { get; set; }
        public List<Like> ImageLikes { get; set; }
        public List<Comment> ImageComments { get; set; }
        public bool isFeatured { get; set; }

        public Image(string imageName,Guid ownerId,string description,string rootPath)
        {
            ImageId=new Guid();
            this.ImageName = imageName;
            ImagePath = rootPath + "/images/" + imageName;
            ImageOwnerId = ownerId;
            ImageDescription = description;
            ImageLikes=new List<Like>();
            ImageComments=new List<Comment>();
            isFeatured = false;
        }

        public Image()
        {
            
        }

        //overridana equals metoda
        public override bool Equals(object obj)
        {
            var item = obj as Image;

            if (item == null)
            {
                return false;
            }

            return this.ImageId.Equals(item.ImageId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.ImageId.GetHashCode();
        }
    }
}