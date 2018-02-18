using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBin.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        //public string email { get; set; }
        public string Name { get; set; }
        public string UserSummary { get; set; }
        public string Surname { get; set; }
        public List<Image> FavoriteImages { get; set; }
        public List<Album> FavoriteAlbums { get; set; }
        public List<User>  SubscribedToList { get; set; }

        //string email, 
        public User(Guid userId,string name, string surname)
        {
            this.UserId = userId;
            //this.email = email;
            this.Name = name;
            this.Surname = surname;
            FavoriteImages=new List<Image>();
            SubscribedToList=new List<User>();
            FavoriteAlbums=new List<Album>();

        }

        public User(Guid userId)
        {
            this.UserId = userId;
            //this.email = email;
            this.Name = "N/A";
            this.Surname = "N/A";
            FavoriteImages = new List<Image>();
            SubscribedToList = new List<User>();
            FavoriteAlbums = new List<Album>();

        }

        public User()
        {
            
        }

        //overridana equals metoda
        public override bool Equals(object obj)
        {
            var item = obj as User;

            if (item == null)
            {
                return false;
            }

            return this.UserId.Equals(item.UserId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.UserId.GetHashCode();
        }

    }
}
