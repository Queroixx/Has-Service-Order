using OsDsII.api.Models;

namespace OsDsII.api.Repository.CommentsRepository
{
    public interface ICommentsRepository
    {
        public Task AddCommentAsync(Comment comment);
    }
}
