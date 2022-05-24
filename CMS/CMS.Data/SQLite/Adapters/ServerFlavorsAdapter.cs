using CMS.Domain.Entities;

namespace CMS.Data.Sqlite.Adapters {
    public class ServerFlavorsAdapter {
        private readonly SQLiteDbContext _dbContext;
        public ServerFlavorsAdapter(SQLiteDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int GetServerFlavorIdByName(string serverFlavorName) {
            try {
                return _dbContext.ServerFlavors
                    .First( serverFlavor => serverFlavor.ServerFlavorName == serverFlavorName)
                    .Id;
            }
            catch (Exception exception) {
                return -1;
            }
        }

        public int GetServerFlavorIdByDisplayName(string displayName) {
            try {
                return _dbContext.ServerFlavors
                    .First(serverFlavor => serverFlavor.DisplayName == displayName)
                    .Id;
            }
            catch (Exception exception) {
                return -1;
            }
        }

        public IEnumerable<ServerFlavor> GetServerFlavors() {
            return _dbContext.ServerFlavors.AsEnumerable();
        }

        public string GetServerFlavorDisplayNameById(int serverFlavorId) {
            return _dbContext.ServerFlavors.First(serverFlavor => serverFlavor.Id == serverFlavorId).DisplayName;
        }

        public string GetServerFlavorNameByDisplayName(string displayName) {
            return _dbContext.ServerFlavors.First(serverFlavor => string.Equals(serverFlavor.DisplayName, displayName)).ServerFlavorName;
        }

        public string GetServerFlavorNameById(int serverFlavorId) {
            return _dbContext.ServerFlavors.First(serverFlavor => serverFlavor.Id == serverFlavorId).ServerFlavorName;
        }

        public int GetMaxRAMById(int serverFlavorId) {
            return _dbContext.ServerFlavors.First(serverFlavor => serverFlavor.Id == serverFlavorId).RAMGBs;

        }
    }
}