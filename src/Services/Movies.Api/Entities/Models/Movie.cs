using System.Reflection;

namespace Movies.Api.Entities.Models
{
    public class Movie
    {
        public Movie(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }
    }
}
