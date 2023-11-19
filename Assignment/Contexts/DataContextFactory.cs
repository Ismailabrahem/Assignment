using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Assignment.Contexts;

internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ismail Abrahem\Desktop\Uppgift\Assignment\Assignment\Contexts\assignment_database.mdf"";Integrated Security=True;Connect Timeout=30");
        return new DataContext(optionsBuilder.Options);
    }
}
