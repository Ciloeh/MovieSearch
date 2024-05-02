using System.ComponentModel.DataAnnotations;

namespace MovieSearchApi.Model
{
	public class Users
	{
		[Key]
        public int Id { get; set; }
        public string phone  { get; set; }
        public string Name { get; set; }
        public DateTime dateadded { get; set; }

		public ICollection<SearchQuery> SearchQueries { get; set; }
	}
}
