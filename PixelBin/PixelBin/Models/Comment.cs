using System;

namespace PixelBin.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid OwnerId { get; set; }
        //public Guid imageId { get; set; }
        public string CommentText { get; set; }

        //, Guid imageId
        public Comment(Guid ownerId, string commentText)
        {
            CommentId=new Guid();
            this.OwnerId = ownerId;
            //this.imageId = imageId;
            this.CommentText = commentText;
        }

        public Comment()
        {
            
        }

        //overridana equals metoda
        public override bool Equals(object obj)
        {
            var item = obj as Comment;

            if (item == null)
            {
                return false;
            }

            return this.CommentId.Equals(item.CommentId);
        }


        //overridana gethashcode metoda
        public override int GetHashCode()
        {
            return this.CommentId.GetHashCode();
        }
    }
}