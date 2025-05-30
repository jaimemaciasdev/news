using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using news.Config;
using news.Models;

namespace news.Services
{
    public class NewsService : INewsService
    {
        private readonly IHackerNewsApiWrapper _hackerNewsApi;
        public NewsService(IHackerNewsApiWrapper hackerNewsApi)
        {
            _hackerNewsApi = hackerNewsApi;
        }

        /// <summary>
        /// Gets the top N best stories from Hacker News
        /// </summary>
        /// <param name="numberOfStories"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsStory>> GetBestStories(int numberOfStories)
        {
            var topStories = await _hackerNewsApi.FetchBestStoryIds();

            List<Task<NewsStory>> storyTasks = [];

            foreach (var id in topStories.Take(numberOfStories))
            {
                storyTasks.Add(_hackerNewsApi.GetStory(id));
            }

            await Task.WhenAll(storyTasks);

            return storyTasks
                .Select(task => task.Result)
                .Where(story => story != null)
                .OrderByDescending(s => s.Score);

        }
    }
}
