using System.Net.Http.Json;

using CMS.Domain.Http.Manifest;

namespace CMS.Data.Http;

public class HttpVersionFetcher {
    private static readonly string versionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest_v2.json";

    private static async Task<List<CMS.Domain.Http.Manifest.Version>> getVersionsFromManifest() {
        using (HttpClient httpClient = new HttpClient()) {
            Manifest? manifest = await httpClient.GetFromJsonAsync<Manifest>(versionManifestUrl);

            return manifest!.versions;
        }
    }
    public static async Task<VersionDetails> getVersionDetails(string name) {
        List<CMS.Domain.Http.Manifest.Version> versionList = await getVersionsFromManifest();
        
        VersionDetails result = new VersionDetails();

        using (HttpClient httpClient = new HttpClient()) {
            foreach(var version in versionList) {
                if(string.Equals(version.id, name)) {
                    VersionDetails? versionDetails = await httpClient.GetFromJsonAsync<VersionDetails>(version.url);
                    return versionDetails!;
                }
            }
        }
        
        return result;
    }
    public static async Task<IEnumerable<VersionDetails>> getVersionDetailsList(int count, bool releasesOnly) {
        List<CMS.Domain.Http.Manifest.Version> versionList = await getVersionsFromManifest();
        
        List<VersionDetails> versionDetailsList = new List<VersionDetails>();
        
        using (HttpClient httpClient = new HttpClient()) {
            foreach(var version in versionList) {
                if (versionDetailsList.Count >= count) break;
                
                VersionDetails? versionDetails = await httpClient.GetFromJsonAsync<VersionDetails>(version.url);
                
                if (releasesOnly) {
                    if (string.Equals(versionDetails!.type, "release")) {
                        versionDetailsList.Add(versionDetails);
                    }
                } 
                else {
                    versionDetailsList.Add(versionDetails!);
                }
            }
        }

        return versionDetailsList.AsEnumerable();
    }
}
