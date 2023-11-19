


using Assignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Contexts;

internal class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<CompanyBranchEntity> CompanyBranches { get; set; }
    public DbSet<CompanyValueEntity> CompanyValues { get; set; }
}
