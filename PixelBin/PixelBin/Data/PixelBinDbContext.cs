using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using PixelBin.Models;

namespace PixelBin.Data
{
    public class PixelBinDbContext : DbContext
    {

        public IDbSet<User> Users { get; set; }
        //public IDbSet<Administrator> Administrators { get; set; }
        public IDbSet<Album> Albums { get; set; }
        public IDbSet<Image> Images { get; set; }
        public  IDbSet<Comment> Comments { get; set; }
        //public IDbSet<Like> Likes { get; set; }


        public PixelBinDbContext(string connectionString) : base(connectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.UserId);
            modelBuilder.Entity<User>().Property(item => item.Name).IsRequired();   
            modelBuilder.Entity<User>().Property(item => item.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(item => item.UserSummary).IsRequired();

            //modelBuilder.Entity<Administrator>().HasKey(admin => admin.userId);

            modelBuilder.Entity<Album>().HasKey(album => album.AlbumId);
            modelBuilder.Entity<Album>().Property(item => item.OwnerId).IsRequired();
            modelBuilder.Entity<Album>().Property(item => item.AlbumName).IsRequired();
            modelBuilder.Entity<Album>().Property(item => item.DateAdded).IsRequired();

            modelBuilder.Entity<Image>().HasKey(img => img.ImageId);
            modelBuilder.Entity<Image>().Property(item => item.ImageOwnerId).IsRequired();
            modelBuilder.Entity<Image>().Property(item => item.ImageName).IsRequired();
            modelBuilder.Entity<Image>().Property(item => item.ImagePath).IsRequired();

            modelBuilder.Entity<Like>().HasKey(like => like.LikeId);
            modelBuilder.Entity<Like>().Property(item => item.UserId).IsRequired();
            //modelBuilder.Entity<Like>().Property(item => item.imageId).IsRequired();

            modelBuilder.Entity<Dislike>().HasKey(like => like.DislikeId);
            modelBuilder.Entity<Dislike>().Property(item => item.UserId).IsRequired();
            //modelBuilder.Entity<Like>().Property(item => item.imageId).IsRequired();

            modelBuilder.Entity<Comment>().HasKey(comment => comment.CommentId);
            modelBuilder.Entity<Comment>().Property(item => item.OwnerId).IsRequired();
            modelBuilder.Entity<Comment>().Property(item => item.CommentText).IsRequired();

            //many to many || one to many
            modelBuilder.Entity<Album>().HasMany(item => item.Images);
            modelBuilder.Entity<Album>().HasMany(item => item.Likes);
            modelBuilder.Entity<Album>().HasMany(item => item.Dislikes);
            modelBuilder.Entity<Album>().HasMany(item => item.Comments);

            modelBuilder.Entity<User>().HasMany(item => item.FavoriteImages);
            modelBuilder.Entity<User>().HasMany(item => item.FavoriteAlbums);
            modelBuilder.Entity<User>().HasMany(item => item.SubscribedToList);

            modelBuilder.Entity<Image>().HasMany(item => item.ImageComments);
            modelBuilder.Entity<Image>().HasMany(item => item.ImageLikes);
            modelBuilder.Entity<Image>().HasMany(item => item.ImageDislikes);


        }
    }
}
