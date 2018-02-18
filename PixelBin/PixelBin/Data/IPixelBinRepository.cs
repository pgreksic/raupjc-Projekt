using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PixelBin.Models;

namespace PixelBin.Data
{
    interface IPixelBinRepository
    {
        Album GetAlbum(Guid albumId);
        Image GetImage(Guid imageId);
        User GetUser(string userId);
        void AddImage(Image image);
        void AddAlbum(Album album);
        void AddCommentToAlbum(Guid albumId, Comment comment);
        void AddCommentToImage(Guid imageid, Comment comment);
        void AddFavorite(ApplicationUser user, Guid albumId);
        void AddUser(User user);
        Task<List<Album>> GetUserAlbums(string UserId);
        List<Image> GetAlbumImages(Guid albumId);
        void RemovePhotoFromAlbum(Guid imageId, Guid albumId);
        Task<List<Album>> FilterAlbums(string filter);
        void AddImagesToAlbum(Guid albumId, IEnumerable<Image> images);
        void LikeImage(Guid imageId, Like like);
        void LikeAlbum(Guid albumId, Like like);
        void DislikeImage(Guid imageId, Dislike dislike);
        void DislikeAlbum(Guid albumId, Dislike dislike);
        List<Like> GetLikes(Guid imageId);
        List<Comment> getComments(Guid imageId);
        Task<List<Album>> GetPinnedAlbums();
        Task<List<Image>> GetFeaturedImages();
        void Dislike(Guid imageId, string userId);
        void UpdateImage(string name, string description, Guid imageId);
        Task<List<Album>> GetAllAlbums();
        void MarkAsPinned(Guid albumId);
        void MarkAsFeatured(Guid imageId);

    }
}
