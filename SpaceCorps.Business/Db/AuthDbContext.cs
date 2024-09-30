using Microsoft.EntityFrameworkCore;

namespace SpaceCorps.Business.Db;

public class AuthDbContext(DbContextOptions options) : DbContext(options)
{

}