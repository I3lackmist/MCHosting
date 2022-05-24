using CMS.Domain.DTOs;
using CMS.Domain.Entities;

namespace CMS.Data.Sqlite.Adapters {
    public class UserGameServersAdapter {
        private readonly SQLiteDbContext _dbContext;
        public UserGameServersAdapter(SQLiteDbContext dbContext) {
            _dbContext = dbContext;
        }
        public IEnumerable<int> GetGameServerIdsByUserId(int userId) {
            return _dbContext.UserGameServers
                .Where(userGameServer => userGameServer.OwnerId == userId)
                .Select(userGameServer => userGameServer.GameServerId)
                .AsEnumerable();
        }

        public void CreateGameServer(int ownerId, int gameServerId) {
            UserGameServer userGameServer = new UserGameServer() {
                OwnerId = ownerId,
                GameServerId = gameServerId,
                OwnerRelativeId = _dbContext.UserGameServers
                    .Where(entry => entry.OwnerId == ownerId)
                    .Count() + 1
            };

            _dbContext.UserGameServers.Add(userGameServer);
            _dbContext.SaveChanges();
        }

        public bool IsServerOwner(int userId, int gameServerId) {
            return _dbContext.UserGameServers
                .First(entry => entry.GameServerId == gameServerId)
                .OwnerId == userId;
        }

        public void DeleteGameServer(int gameServerId) {
            UserGameServer? userGameServer = _dbContext.UserGameServers.First(entry => entry.GameServerId == gameServerId);

            _dbContext.UserGameServers.Remove(userGameServer);
            _dbContext.SaveChanges();
        }
    }
}