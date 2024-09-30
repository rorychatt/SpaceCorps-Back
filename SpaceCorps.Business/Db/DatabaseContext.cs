using Microsoft.EntityFrameworkCore;
using SpaceCorps.Business.Authorization;

namespace SpaceCorps.Business.Db;

public partial class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserCredential> UserCredentials { get; set; }
}