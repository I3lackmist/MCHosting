DROP TABLE IF EXISTS User_GameServers;
DROP TABLE IF EXISTS GameServers;
DROP TABLE IF EXISTS ServerFlavors;
DROP TABLE IF EXISTS Users;

CREATE TABLE IF NOT EXISTS Users (
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
   	UserName TEXT NOT NULL UNIQUE,
	Email TEXT NOT NULL UNIQUE,
    Password TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS ServerFlavors (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ServerFlavorName TEXT NOT NULL UNIQUE,
    DisplayName TEXT NOT NULL UNIQUE,
    CPUs INTEGER NOT NULL,
    RAMGBs INTEGER NOT NULL,
    MemoryGBs INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS GameServers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    GameServerName TEXT NOT NULL,
    ServerFlavorId INTEGER NOT NULL,
    GameVersionName TEXT NOT NULL,
    Ip TEXT,
    FOREIGN KEY(ServerFlavorId) REFERENCES ServerFlavors(Id)
);

CREATE TABLE IF NOT EXISTS User_GameServers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OwnerId INTEGER NOT NULL,
    GameServerId INTEGER NOT NULL,
    OwnerRelativeId INTEGER NOT NULL,
    FOREIGN KEY(OwnerId) REFERENCES Users(Id),
    FOREIGN KEY(GameServerId) REFERENCES GameServers(Id)
);

INSERT INTO ServerFlavors (ServerFlavorName, DisplayName, CPUs, RAMGBs, MemoryGBs) VALUES ("m1.tiny", "Test", 1, 1, 1);
INSERT INTO ServerFlavors (ServerFlavorName, DisplayName, CPUs, RAMGBs, MemoryGBs) VALUES ("4c4g20g", "Light", 4, 4, 20);
INSERT INTO ServerFlavors (ServerFlavorName, DisplayName, CPUs, RAMGBs, MemoryGBs) VALUES ("4c8g30g", "Standard", 4, 8, 30);
INSERT INTO ServerFlavors (ServerFlavorName, DisplayName, CPUs, RAMGBs, MemoryGBs) VALUES ("4c16g40g", "Premium", 4, 16, 40);