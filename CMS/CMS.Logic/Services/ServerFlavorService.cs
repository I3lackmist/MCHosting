using CMS.Data.Sqlite.Adapters;
using CMS.Domain.DTOs;
using CMS.Domain.Entities;

public class ServerFlavorService {
    private ServerFlavorsAdapter _serverFlavorsAdapter;
    
    public ServerFlavorService(ServerFlavorsAdapter serverFlavorsAdapter) {
        _serverFlavorsAdapter = serverFlavorsAdapter;
    }

    public IEnumerable<ServerFlavorDTO> GetServerFlavors() {
        return _serverFlavorsAdapter.GetServerFlavors()
            .Select<ServerFlavor, ServerFlavorDTO>(
                serverFlavor => new ServerFlavorDTO() {
                    displayName = serverFlavor.DisplayName,
                    cpus = serverFlavor.CPUs,
                    ramGBs = serverFlavor.RAMGBs,
                    memoryGBs = serverFlavor.MemoryGBs
                }
            );
    }
}