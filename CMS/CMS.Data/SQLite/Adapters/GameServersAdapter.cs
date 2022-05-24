using CMS.Domain.Entities;

namespace CMS.Data.Sqlite.Adapters {
    public class GameServersAdapter {
        private readonly SQLiteDbContext _dbContext;
        public GameServersAdapter(SQLiteDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int CreateGameServer(int ownerId, string gameServerName, int serverFlavorId, string gameVersionName, string ip) {
            GameServer gameServer = new GameServer() {
                GameServerName = gameServerName,
                ServerFlavorId = serverFlavorId,
                GameVersionName = gameVersionName,
                Ip = ip
            };

            GameServer newGameServer = _dbContext.GameServers.Add(gameServer).Entity;
            _dbContext.SaveChanges();

            return newGameServer.Id;
        }

        public bool IsNameTaken(string gameServerName) {
            return _dbContext.GameServers.Any(gameServer => string.Equals(gameServer.GameServerName, gameServerName));
        }

        public IEnumerable<GameServer> GetGameServersByIds(IEnumerable<int> gameServerIds) {
            return _dbContext.GameServers.Where(gameServer => gameServerIds.Contains(gameServer.Id));
        }

        public int GetGameServerIdByName(string gameServerName) {
            try {
                return _dbContext.GameServers
                    .First(gameserver => gameserver.GameServerName == gameServerName)
                    .Id;
            } catch (Exception exception) {
                return -1;
            }
        }

        public void ChangeGameServerFlavor(int gameServerId, int flavorId) {
            GameServer? gameServer = _dbContext.GameServers.Find(gameServerId);

            if (gameServer != null) {
                gameServer.ServerFlavorId = flavorId;
                _dbContext.SaveChanges();
            }
        }
        public GameServer GetGameServerById(int gameServerId) {
            return _dbContext.GameServers.Find(gameServerId)!;
        }

        public void ChangeGameVersion(int gameServerId, string gameVersionName) {
            GameServer? gameServer = _dbContext.GameServers.Find(gameServerId);

            if (gameServer != null) {
                gameServer.GameVersionName = gameVersionName;
                _dbContext.SaveChanges();
            }
        }

        public void ChangeGameServerName(int gameServerId, string newName) {
            GameServer? gameServer = _dbContext.GameServers.Find(gameServerId);

            if (gameServer != null) {
                gameServer.GameServerName = newName;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteGameServer(int gameServerId) {
            GameServer gameServer = _dbContext.GameServers.Find(gameServerId);

            _dbContext.GameServers.Remove(gameServer);
            _dbContext.SaveChanges();
        }
    }
}