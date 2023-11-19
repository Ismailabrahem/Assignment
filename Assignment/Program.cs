using Assignment.Contexts;
using Assignment.Menu;
using Assignment.Repositories;
using Assignment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ismail Abrahem\Desktop\Uppgift\Assignment\Assignment\Contexts\assignment_database.mdf"";Integrated Security=True;Connect Timeout=30"));

            // Repositories
            services.AddScoped<AddressRepository>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<CompanyRepository>();
            services.AddScoped<CompanyBranchRepository>();
            services.AddScoped<CompanyValueRepository>();
            
            // Services
            services.AddScoped<ClientService>();
            services.AddScoped<CompanyService>();

            // Menus
            services.AddScoped<ClientMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<CompanyMenu>();
            
            
            
            
            
            
            var sp = services.BuildServiceProvider();
            var mainMenu = sp.GetRequiredService<MainMenu>();
            await mainMenu.StartAsync();
                   
        }
    }
}