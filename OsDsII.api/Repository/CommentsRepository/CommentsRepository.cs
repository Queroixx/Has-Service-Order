using OsDsII.api.Data;
using OsDsII.api.Models;

namespace OsDsII.api.Repository.CommentsRepository
{
    public sealed class CommentsRepository : ICommentsRepository
    {
        private readonly DataContext _dataContext;

        public CommentsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _dataContext.Comments.AddAsync(comment); // This line adds the comment to the context
            await _dataContext.SaveChangesAsync();
        }
    }
}
