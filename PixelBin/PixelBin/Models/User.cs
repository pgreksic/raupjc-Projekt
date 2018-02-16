using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBin.Models
{
    public class User
    {
        public Guid userId { get; set; }
        //public string email { get; set; }
        public string Name { get; set; }
        public string UserSummary { get; set; }
        public string Surname { get; set; }
        public List<Image> FavoriteImages { get; set; }
        public List<Album> FavoriteAlbums { get; set; }
        public List<User>  SubscribedToList { get; set; }

        //string email, 
        public User(string name, string surname)
        {
            userId=new Guid();
            //this.email = email;
            this.Name = name;
            this.Surname = surname;
            FavoriteImages=new List<Image>();
            SubscribedToList=new List<User>();
            FavoriteAlbums=new List<Album>();

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

            return this.userId.Equals(item.userId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.userId.GetHashCode();
        }

    }
}
