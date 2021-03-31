using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class UserAuthorMapper : IMapper<Author, User>
    {
        public Author ToBlModel(User user)
        {
            return new Author
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public User ToDalModel(Author author)
        {
            return new User
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email
            };
        }
    }
}
