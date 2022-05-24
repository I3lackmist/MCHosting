using Microsoft.EntityFrameworkCore;

using CMS.Domain.Entities;

namespace CMS.Data.Sqlite;

public class SQLiteDbContext : DbContext {
    public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options): base(options) {}
    public DbSet<GameServer> GameServers { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<GameVersion> GameVersions { get; set; } = null!;
    public DbSet<ServerFlavor> ServerFlavors { get; set; } = null!;
    public DbSet<UserGameServer> UserGameServers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) {}
}
