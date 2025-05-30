namespace news.Config
{
    public class HackerNewsApiConfig
    {
        public const string Section = "HackerNewsApi";
        public string BaseUrl { get; set; } = string.Empty;
        public int CacheDurationMinutes { get; set; } = 5;
    }
}
