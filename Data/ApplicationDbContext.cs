using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Protrac1.Models;
using ProtracV1.Models;

namespace ProtracV1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<JobStartForm> JobStartForm { get; set; } = default!;

public DbSet<Protrac1.Models.InputRegister> InputRegister { get; set; } = default!;

// public DbSet<Protrac1.Models.MyViewModel> MyViewModel { get; set; } = default!;

}
