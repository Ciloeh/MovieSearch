using System.ComponentModel.DataAnnotations;

namespace MovieSearchApi.Model
{
    public class SearchQuery
    {
        [Key]
        public int Id { get; set; }
        public string Query { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
