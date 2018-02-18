using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBin.Models
{
    public class Album
    {
        public Guid AlbumId { get; set; }
        public Guid OwnerId { get; set; }
        public string AlbumName { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public List<Dislike> Dislikes { get; set; }
        public bool isPinned { get; set; }
        public DateTime DateAdded { get; set; }


        public Album(Guid ownerId, string albumName, string description)
        {
            AlbumId=new Guid();
            this.AlbumName = albumName;
            this.OwnerId = ownerId;
            this.Description = description;
            Images=new List<Image>();
            Comments=new List<Comment>();
            Likes=new List<Like>();
            Dislikes=new List<Dislike>();
            DateAdded=DateTime.Now;
        }

        public Album()
        {
            
        }

        //overridana equals metoda
        public override bool Equals(object obj)
        {
            var item = obj as Album;

            if (item == null)
            {
                return false;
            }

            return this.AlbumId.Equals(item.AlbumId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.AlbumId.GetHashCode();
        }
    }
}
