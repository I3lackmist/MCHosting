using System.Net.Http.Json;

using CMS.Domain.DTOs;
using CMS.Domain.Types;

namespace CMS.Data.Http;

public class HttpServerStateChecker {
    private readonly static string IOA_BASE_URL = "http://127.0.0.1:20004/server/";

    public async static Task<string> GetServerStatus(string serverName) {
        using (HttpClient httpClient = new HttpClient()) {
            SimpleResponse? responseBuffer;

            HttpResponseMessage statusResponse = await httpClient.GetAsync(
                string.Concat(IOA_BASE_URL, "/status?name=", serverName)
            );

            responseBuffer = await statusResponse.Content.ReadFromJsonAsync<SimpleResponse>();

            if (!statusResponse.IsSuccessStatusCode) {
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }

            return responseBuffer.message;
        }
    }
}