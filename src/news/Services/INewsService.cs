using news.Models;

namespace news.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsStory>> GetBestStories(int numberOfStories);
    }
}
