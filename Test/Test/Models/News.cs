using System.Collections.Generic;

namespace Test.Models
{
    public class NewsResult
    {
        public string status { get; set; }
        public int totalResults{ get; set; }
        public List<News> results { get; set; }
        public int nextPage { get; set; }
    }

    public class News
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string pubDate { get; set; }
        public string image_url { get; set; }
        public string source_id { get; set; }
    }
}