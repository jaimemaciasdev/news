namespace news.Models
{
    public class NewsStory
    {
        /// <summary>
        /// The story Title
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// URL of the story
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// The author of the story
        /// </summary>
        public string By { get; set; } = string.Empty;
        /// <summary>
        /// The date and time the story was created, represented as a Unix timestamp
        /// </summary>
        public long Time { get; set; } = 0L;
        /// <summary>
        /// Ranking of the story based on its score
        /// </summary>
        public int Score { get; set; } = 0;
        /// <summary>
        /// The number of comments on the story
        /// </summary>
        public int Descendants { get; set; } = 0;
    }
}
