using CMS.Domain.DTOs;
using CMS.Domain.Http.Manifest;
using CMS.Data.Http;

namespace CMS.Logic.Services;
public class GameVersionService {
    private List<GameVersionDTO> gameVersionList = new List<GameVersionDTO>();
    private int lastVersionIndex = 0;
    public GameVersionService() {
        Task.Run(() => getGameVersions());
    }

    public async Task<IEnumerable<GameVersionDTO>> getGameVersions() {
        if (gameVersionList.Count() == 0) {
            IEnumerable<VersionDetails> versionDetailsCollection = await HttpVersionFetcher.getVersionDetailsList(15, true);
            
            foreach (var versionDetails in versionDetailsCollection) {
                GameVersionDTO gameVersion = new GameVersionDTO() { gameVersionName = versionDetails.id };
                gameVersionList.Add(gameVersion);
            }

            lastVersionIndex++;
        }

        return gameVersionList.AsEnumerable();
    }

    public async Task<string> getGameVersionDownloadURL(string versionName) {
        VersionDetails details = await HttpVersionFetcher.getVersionDetails(versionName);
        
        return details.downloads.server.url;
    }

} 