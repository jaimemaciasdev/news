using news.Models;

namespace news.Services
{
    public interface IHackerNewsApiWrapper
    {
        public Task<IEnumerable<int>> FetchBestStoryIds();
        public Task<NewsStory> GetStory(int id);
    }
}
