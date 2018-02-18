using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PixelBin.Models;

namespace PixelBin.Data
{
    public class PixelBinRepository : IPixelBinRepository
    {
        private readonly PixelBinDbContext _dbContext;

        public PixelBinRepository(PixelBinDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Album GetAlbum(Guid albumId)
        {
            return _dbContext.Albums.Where(album => album.AlbumId.Equals(albumId)).Include(album => album.Images).Include(album => album.Comments).FirstOrDefault();
        }

        public Image GetImage(Guid imageId)
        {
            return _dbContext.Images.Where(image => image.ImageId.Equals(imageId)).Include(img => img.ImageComments)
                .Include(img => img.ImageLikes).FirstOrDefault();
        }

        public User GetUser(string userId)
        {
            Guid compare=new Guid(userId);

            return _dbContext.Users.Where(user => user.UserId.Equals(compare)).Include(user => user.FavoriteAlbums)
                .Include(user => user.FavoriteImages).Include(user => user.SubscribedToList).FirstOrDefault();
        }

        public void AddImage(Image image)
        {
            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();
        }

        public void AddAlbum(Album album)
        {
            _dbContext.Albums.Add(album);
            _dbContext.SaveChanges();
        }

        public void AddCommentToAlbum(Guid albumId, Comment comment)
        {
            Album tmp = GetAlbum(albumId);
            tmp.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void AddCommentToImage(Guid imageid, Comment comment)
        {
            Image img = GetImage(imageid);
            img.ImageComments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void AddFavorite(ApplicationUser user, Guid albumId)
        {
            User tmp = GetUser(user.Id);
            Album album = GetAlbum(albumId);
            tmp.FavoriteAlbums.Add(album);
            _dbContext.SaveChanges();
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public async Task<List<Album>> GetUserAlbums(string UserId)
        {
            return await _dbContext.Albums
                .Where(album=> album.OwnerId.Equals(new Guid(UserId)))
                .Include(album => album.Images)
                .ToListAsync();
        }

        public List<Image> GetAlbumImages(Guid albumId)
        {
            
            return GetAlbum(albumId)?.Images;
        }

        public void RemovePhotoFromAlbum(Guid imageId, Guid albumId)
        {
            Album album = GetAlbum(albumId);
            album.Images.Remove(GetImage(imageId));
            _dbContext.SaveChanges();
        }

        public async Task<List<Album>> FilterAlbums(string filter)
        {
            return await _dbContext.Albums
                .Where(album => album.AlbumName.Trim().ToLower().Contains(filter.Trim().ToLower()) || album.Description.Trim().Contains(filter.Trim().ToLower()))
                .Include(i => i.Images).ToListAsync();

        }

        public void AddImagesToAlbum(Guid albumId, IEnumerable<Image> images)
        {
            Album album = GetAlbum(albumId);
            foreach (Image img  in images)
            {
                album.Images.Add(img);
            }
        }

        public void LikeImage(Guid imageId, Like like)
        {
            Image img = GetImage(imageId);
            img.ImageLikes.Add(like);
        }

        public void LikeAlbum(Guid albumId, Like like)
        {
            Album album = GetAlbum(albumId);
            album.Likes.Add(like);
        }

        public void DislikeImage(Guid imageId, Dislike dislike)
        {
            Image img = GetImage(imageId);
            img.ImageDislikes.Add(dislike);
        }

        public void DislikeAlbum(Guid albumId, Dislike dislike)
        {
            Album album = GetAlbum(albumId);
            album.Dislikes.Add(dislike);
        }

        public List<Like> GetLikes(Guid imageId)
        {
            return GetImage(imageId).ImageLikes;
        }

        public List<Comment> getComments(Guid imageId)
        {
            return GetImage(imageId).ImageComments;
        }

        public async Task<List<Album>> GetPinnedAlbums()
        {
            return await _dbContext.Albums.Where(album => album.isPinned.Equals(true)).Include(album => album.Images)
                .Include(album => album.Comments).Include(album => album.Likes).ToListAsync();
        }

        public async Task<List<Image>> GetFeaturedImages()
        {
            return await _dbContext.Images.Where(image => image.isFeatured.Equals(true)).Include(image => image.ImageLikes).Include(image => image.ImageComments)
                .ToListAsync();
        }

        public void Dislike(Guid imageId, string userId)
        {
            Guid userIdCompare=new Guid(userId);
            Image img = GetImage(imageId);
            Like like = img.ImageLikes?.Where(image => image.UserId.Equals(userIdCompare)).FirstOrDefault();
            img.ImageLikes?.Remove(like);
            _dbContext.SaveChanges();
        }

        public void UpdateImage(string name, string description, Guid imageId)
        {
            Image img = GetImage(imageId);
            img.ImageName = name;
            img.ImageDescription = description;
            _dbContext.SaveChanges();
        }

        public async Task<List<Album>> GetAllAlbums()
        {
            return await _dbContext.Albums.Include(album => album.Images).ToListAsync();
        }

        public void MarkAsPinned(Guid albumId)
        {
            Album album = GetAlbum(albumId);
            album.isPinned = true;
        }

        public void MarkAsFeatured(Guid imageId)
        {
            Image img = GetImage(imageId);
            img.isFeatured = true;
        }
    }
}
