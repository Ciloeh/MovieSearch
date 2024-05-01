using MovieSearchApi.Model;

namespace MovieSearchApi.Services
{
    public class SearchResult
    {
        public List<MoviesDeatils> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }
}
