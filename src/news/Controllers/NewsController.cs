using Microsoft.AspNetCore.Mvc;
using news.Models;
using news.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace news.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService) 
        {
            _newsService = newsService;
        }
        // GET: api/top?n=5
        [HttpGet("top")]
        public async Task<IEnumerable<NewsStory>> GetTopStories(int n = 10)
        {
            return await _newsService.GetBestStories(n);
        }
    }
}
