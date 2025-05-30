using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using news.Config;
using news.Models;

namespace news.Services
{
    public class HackerNewsApiWrapper : IHackerNewsApiWrapper
    {
        private readonly HttpClient _httpClient;
        private readonly HackerNewsApiConfig _config;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<HackerNewsApiWrapper> _logger;

        public HackerNewsApiWrapper(
            IOptions<HackerNewsApiConfig> config,
            IHttpClientFactory clientFactory,
            IMemoryCache memoryCache,
            ILogger<HackerNewsApiWrapper> logger)
        {
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config), "Configuration cannot be null");
            _httpClient = clientFactory.CreateClient("NewsApiClient");
            _httpClient.BaseAddress = new Uri(config.Value.BaseUrl);
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<NewsStory> GetStory(int id)
        {
            try
            {
                _logger.LogInformation("Fetching story with ID {StoryId}", id);

                var story = await _memoryCache.GetOrCreateAsync($"HN_Story_{id}", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.CacheDurationMinutes);
                    return await _httpClient.GetFromJsonAsync<NewsStory>($"item/{id}.json");
                });

                if (story == null)
                {
                    _logger.LogWarning("Story with ID {StoryId} not found.", id);
                    throw new Exception($"Error while fetching story with ID {id}");
                }

                return story;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching story with ID {StoryId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<int>> FetchBestStoryIds()
        {
            try
            {
                _logger.LogInformation("Fetching best story IDs.");

                var ids = await _memoryCache.GetOrCreateAsync("HN_BestStories", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.CacheDurationMinutes);
                    return await _httpClient.GetFromJsonAsync<int[]>("beststories.json");
                });

                if (ids == null)
                {
                    _logger.LogWarning("No best story IDs found.");
                    throw new Exception("Error while fetching top stories");
                }

                return ids;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while fetching best story IDs.");
                throw;
            }
        }
    }
}
