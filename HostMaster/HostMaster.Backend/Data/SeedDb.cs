using HostMaster.Backend.Helpers;
using HostMaster.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HostMaster.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context, IFileStorage fileStorage)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckDbAsync();
    }

    private async Task CheckDbAsync()
    {
        if (!_context.Rooms.Any()) { 
            var SQLScript = File.ReadAllText("Data\\Seed.sql");
            await _context.Database.ExecuteSqlRawAsync(SQLScript);
        }
    }
}