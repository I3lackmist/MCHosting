using CMS.Data.Sqlite.Adapters;
using CMS.Domain.DTOs;

namespace CMS.Logic.Services;
public class UsersService {
    private UsersAdapter _usersAdapter;
    public UsersService(UsersAdapter usersAdapter) {
        _usersAdapter = usersAdapter;
    }

    public void LoginUser(LoginUserRequestDTO request) {
        if (!_usersAdapter.UserExists(request.userName)) {
            throw new ArgumentException("No such user registered.");
        }
        
        if (!_usersAdapter.UserCredentialsCorrect(request.userName, request.password)) {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }

    public void RegisterUser(RegisterUserRequestDTO request) {
        if (!_usersAdapter.EmailFree(request.email)) {
            throw new ArgumentException("Email already taken.");
        }
        if (!_usersAdapter.UserNameFree(request.userName)) {
            throw new ArgumentException("Username already taken.");
        }

        _usersAdapter.RegisterUser(request);
    }
}
