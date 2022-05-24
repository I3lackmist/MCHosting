using CMS.Domain.Entities;
using CMS.Domain.DTOs;

namespace CMS.Data.Sqlite.Adapters {
    public class UsersAdapter {
        private readonly SQLiteDbContext _dbContext;
        public UsersAdapter(SQLiteDbContext dbContext) {
            _dbContext = dbContext;
        }

        public bool UserExists(string userName) {
            return _dbContext.Users
                .Any(entry => entry.UserName == userName);
        }

        public bool UserCredentialsCorrect(string userName, string userPassword) {
            return _dbContext.Users
                .Any(entry => entry.UserName == userName && entry.Password == userPassword);
        }

        public bool UserNameFree(string userName) {
            return !_dbContext.Users
                .Any(entry => entry.UserName == userName);
        }

        public bool EmailFree(string email) {
            return !_dbContext.Users
                .Any(entry => entry.Email == email);
        }

        public void RegisterUser(RegisterUserRequestDTO registerringUserData) {
            User user = new User() {
                UserName = registerringUserData.userName,
                Email = registerringUserData.email,
                Password = registerringUserData.password
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public int GetUserId(string userName) {
            return _dbContext.Users.First(entry => entry.UserName == userName).Id;
        }
    }
}