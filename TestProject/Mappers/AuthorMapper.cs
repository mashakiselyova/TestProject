using TestProject.Models;

namespace TestProject.Mappers
{
    public class AuthorMapper : IMapper<Author, BL.Models.Author>
    {
        public BL.Models.Author ToBlModel(Author author)
        {
            return new BL.Models.Author
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email
            };
        }

        public Author ToWebModel(BL.Models.Author author)
        {
            return new Author
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email
            };
        }
    }
}
