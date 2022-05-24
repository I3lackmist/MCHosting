using System.Text.Json;
using System.Net.Http.Json;

using CMS.Data.Sqlite.Adapters;
using CMS.Data.Http;
using CMS.Domain.DTOs;
using CMS.Domain.Entities;
using CMS.Domain.Types;

namespace CMS.Logic.Services;
public class GameServerService {
    private GameServersAdapter  _gameServersAdapter;
    private UsersAdapter _usersAdapter;
    private UserGameServersAdapter _userGameServersAdapter;
    private ServerFlavorsAdapter _serverFlavorsAdapter;
    private GameVersionService _gameVersionService;

    private readonly string IOA_BASE_URL = "http://127.0.0.1:20004/server/";
    private readonly string JDK_17_URL = "https://download.oracle.com/java/17/archive/jdk-17.0.3.1_linux-x64_bin.deb";

    public GameServerService(
        GameServersAdapter gameServersAdapter,
        UsersAdapter usersAdapter,
        UserGameServersAdapter userGameServersAdapter,
        ServerFlavorsAdapter serverFlavorsAdapter,
        GameVersionService gameVersionService
    ) {
        _gameServersAdapter = gameServersAdapter;
        _usersAdapter = usersAdapter;
        _userGameServersAdapter = userGameServersAdapter;
        _serverFlavorsAdapter = serverFlavorsAdapter;
        _gameVersionService = gameVersionService;
    }

    public async Task CreateGameServer(CreateGameServerRequestDTO request) {
        if (!_usersAdapter.UserExists(request.requestingUserName)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        if (_gameServersAdapter.IsNameTaken(request.gameServerName)) {
            throw new ArgumentException("Name taken.");
        }

        int userId = _usersAdapter.GetUserId(request.requestingUserName);
        string serverFlavorName = _serverFlavorsAdapter.GetServerFlavorNameByDisplayName(request.serverFlavorName);
        int serverFlavorId = _serverFlavorsAdapter.GetServerFlavorIdByName(serverFlavorName);
        int maxRAM = _serverFlavorsAdapter.GetMaxRAMById(serverFlavorId) * 1024;
        string gameVersionName = request.gameVersionName;
        string jarDownloadUrl = await _gameVersionService.getGameVersionDownloadURL(request.gameVersionName);
        string javaDownloadUrl = JDK_17_URL;

        SimpleResponse? responseBuffer;

        using (HttpClient httpClient = new HttpClient()) {
            Dictionary<string, string> bodyFields = new Dictionary<string, string>() {
                {"name", request.gameServerName},
                {"password", "secret"},
                {"flavor", serverFlavorName},
                {"version", request.gameVersionName},
                {"jarDownloadUrl", jarDownloadUrl},
                {"javaDownloadUrl", javaDownloadUrl},
                {"maxRam", maxRAM.ToString()}
            };
            
            StringContent body = new StringContent(
                JsonSerializer.Serialize(bodyFields),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage serverCreateResponse = await httpClient.PostAsync(
                string.Concat(IOA_BASE_URL, "/create"),
                body
            );

            responseBuffer = await serverCreateResponse.Content.ReadFromJsonAsync<SimpleResponse>();

            if (!serverCreateResponse.IsSuccessStatusCode) {
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }

            HttpResponseMessage serverIpResponse = await httpClient.GetAsync(
                string.Concat(IOA_BASE_URL, "/ip?name=", request.gameServerName)
            );

            responseBuffer = await serverIpResponse.Content.ReadFromJsonAsync<SimpleResponse>();

            if (!serverIpResponse.IsSuccessStatusCode) {
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }

            int newGameServerId = _gameServersAdapter.CreateGameServer(userId, request.gameServerName, serverFlavorId, gameVersionName, responseBuffer?.message);
            _userGameServersAdapter.CreateGameServer(userId, newGameServerId);
        }
    }

    public IEnumerable<GameServerDTO> GetUsersGameServers(string userName) {
        int userId = _usersAdapter.GetUserId(userName);

        IEnumerable<int> usersGameServersIds = _userGameServersAdapter.GetGameServerIdsByUserId(userId);

        IEnumerable<GameServerDTO> result = _gameServersAdapter.GetGameServersByIds(usersGameServersIds)
            .Select( gameServer => new GameServerDTO() {
                gameServerName = gameServer.GameServerName,
                gameVersionName = gameServer.GameVersionName,
                serverFlavorName = _serverFlavorsAdapter.GetServerFlavorDisplayNameById(gameServer.ServerFlavorId),
                ip = gameServer.Ip
            }
        );

        return result;
    }

    public async Task ChangeGameServerName(ChangeGameServerNameRequestDTO request) {
        if (!_usersAdapter.UserExists(request.requestingUserName)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        int userId = _usersAdapter.GetUserId(request.requestingUserName);
        int gameServerId = _gameServersAdapter.GetGameServerIdByName(request.serverName);

        if (!_userGameServersAdapter.IsServerOwner(userId, gameServerId)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        if (_gameServersAdapter.IsNameTaken(request.newName)) {
            throw new ArgumentException("Name already taken.");
        }

        using (HttpClient httpClient = new HttpClient()) {
            Dictionary<string,string> bodyFields = new Dictionary<string, string>() {
                {"name", request.serverName},
                {"newname", request.newName}
            };

            StringContent body = new StringContent(
                JsonSerializer.Serialize(bodyFields),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage nameChangeResponse = await httpClient.PutAsync(
                string.Concat(IOA_BASE_URL, "/name"),
                body
            );

            SimpleResponse? responseBuffer;

            if (!nameChangeResponse.IsSuccessStatusCode) {
                responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await nameChangeResponse.Content.ReadAsStringAsync());
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }

            responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await nameChangeResponse.Content.ReadAsStringAsync());

            _gameServersAdapter.ChangeGameServerName(gameServerId, request.newName);
        }
    }

    public async Task ChangeGameServerVersion(ChangeGameServerVersionRequestDTO request) {
        if (!_usersAdapter.UserExists(request.requestingUserName)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        int userId = _usersAdapter.GetUserId(request.requestingUserName);
        int gameServerId = _gameServersAdapter.GetGameServerIdByName(request.serverName);

        if (!_userGameServersAdapter.IsServerOwner(userId, gameServerId)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        using (HttpClient httpClient = new HttpClient()) {
            Dictionary<string,string> bodyFields = new Dictionary<string, string>() {
                {"name", request.serverName},
                {"version", request.newVersion}
            };

            StringContent body = new StringContent(
                JsonSerializer.Serialize(bodyFields),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage nameChangeResponse = await httpClient.PutAsync(
                string.Concat(IOA_BASE_URL, "/version"),
                body
            );

            SimpleResponse? responseBuffer;

            if (!nameChangeResponse.IsSuccessStatusCode) {
                responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await nameChangeResponse.Content.ReadAsStringAsync());
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }

            responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await nameChangeResponse.Content.ReadAsStringAsync());
            _gameServersAdapter.ChangeGameVersion(gameServerId, request.newVersion);
        }
    }

    public async Task ChangeGameServerFlavor(ChangeGameServerFlavorRequestDTO request) {
        if (!_usersAdapter.UserExists(request.requestingUserName)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        int userId = _usersAdapter.GetUserId(request.requestingUserName);
        int gameServerId = _gameServersAdapter.GetGameServerIdByName(request.serverName);
        int flavorId = _serverFlavorsAdapter.GetServerFlavorIdByDisplayName(request.newFlavor);

        if (!_userGameServersAdapter.IsServerOwner(userId, gameServerId)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        if (flavorId == -1) {
            throw new ArgumentException("Invalid flavor.");
        }

        using (HttpClient httpClient = new HttpClient()) {
            Dictionary<string,string> bodyFields = new Dictionary<string, string>() {
                {"name", request.serverName},
                {"flavor", request.newFlavor}
            };

            StringContent body = new StringContent(
                JsonSerializer.Serialize(bodyFields),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage nameChangeResponse = await httpClient.PutAsync(
                string.Concat(IOA_BASE_URL, "/flavor"),
                body
            );

            SimpleResponse? responseBuffer;

            if (!nameChangeResponse.IsSuccessStatusCode) {
                responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await nameChangeResponse.Content.ReadAsStringAsync());
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }

            responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await nameChangeResponse.Content.ReadAsStringAsync());
            _gameServersAdapter.ChangeGameServerFlavor(gameServerId, flavorId);
        }
    }

    public async Task<GameServerDTO> GetGameServer(string gameServerName) {
        int gameServerId = _gameServersAdapter.GetGameServerIdByName(gameServerName);

        GameServer gameServer = _gameServersAdapter.GetGameServerById(gameServerId);

        return new GameServerDTO() {
            gameServerName = gameServer.GameServerName,
            gameVersionName = gameServer.GameVersionName,
            serverFlavorName = _serverFlavorsAdapter.GetServerFlavorNameById(gameServer.ServerFlavorId),
            ip = gameServer.Ip,
            status = await HttpServerStateChecker.GetServerStatus(gameServerName)
        };
    }

    public async Task DeleteGameServer(string gameServerName, string requestingUserName) {
        if (!_usersAdapter.UserExists(requestingUserName)) {
            throw new ArgumentException("Unauthorized.");
        }

        int userId = _usersAdapter.GetUserId(requestingUserName);
        int gameServerId = _gameServersAdapter.GetGameServerIdByName(gameServerName);
        
        string serverIp = _gameServersAdapter.GetGameServerById(gameServerId).Ip;

        if (gameServerId == -1) {
            throw new ArgumentException("Unauthorized.");
        }

        if (!_userGameServersAdapter.IsServerOwner(userId, gameServerId)) {
            throw new UnauthorizedAccessException("Unauthorized.");
        }

        using (HttpClient httpClient = new HttpClient()) {
            SimpleResponse? responseBuffer;
            HttpResponseMessage serverDeleteResponse = await httpClient.DeleteAsync(
                string.Concat(
                    IOA_BASE_URL+"/delete?name=",
                    gameServerName,
                    "&ip=",
                    serverIp
                )
            );

            if (!serverDeleteResponse.IsSuccessStatusCode) {
                responseBuffer = JsonSerializer.Deserialize<SimpleResponse>(await serverDeleteResponse.Content.ReadAsStringAsync());
                throw new InternalServiceException(responseBuffer?.message ?? "Internal service error.");
            }
        }

        _userGameServersAdapter.DeleteGameServer(gameServerId);
        _gameServersAdapter.DeleteGameServer(gameServerId);
    }
}
