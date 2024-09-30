using Microsoft.EntityFrameworkCore;
using SpaceCorps.Business.Authorization;

namespace SpaceCorps.Business.Db;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    private DbSet<UserCredential> UserCredentials { get; set; }
}